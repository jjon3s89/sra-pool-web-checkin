using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPoolCheckin.Models;
using WebPoolCheckin.Areas.Search.Models;
using System.Net.Mail;

namespace WebPoolCheckin.Areas.Search.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        
        public ActionResult Index()
        {
            return View("Search");
        }
        
        public ActionResult Search(String searchText)
        {
            if (String.IsNullOrWhiteSpace(searchText))
            {
                return View();
            }
            int shareId = -1;
            if (searchText.Length > 0 && !Int32.TryParse(searchText, out shareId))
            {
                ModelState.AddModelError("ShareText", "Entry must be numerical");
                return View();
            }
            if (shareId == 99999)
            {
                return Redirect("~/Report");
            }
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            if(!ctx.Shares.Any(s=>s.Id==shareId && s.Active==true)){
                if (ctx.Employees.Any(e => e.Id == shareId && e.Active == true))
                {
                    return RedirectToAction("Checkin", "Employee", new { id = shareId });
                }
                ModelState.AddModelError("ShareText", "Active Membership not Found");
                return View();
            }
            var share = from s in ctx.Shares where s.Id==shareId && s.Active==true select s;
            var activeShareFamilies = share.SelectMany(s=>s.ShareFamilies).Where(sf=>sf.Active);
            
            return View("Search",share.Single());
        }
        public ActionResult Checkin(int Id, int[] CheckinPeople)
        {
            if (CheckinPeople == null || CheckinPeople.Length == 0)
            {
                ModelState.AddModelError("CheckinPeople","You must select a member to check in");
                return Search(Id.ToString());
            }
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            var checkinPersonObjects = new List<Person>();
            foreach (var pid in CheckinPeople)
            {
                checkinPersonObjects.Add(ctx.People.Single(p => p.Id == pid));
            }
            if (!checkinPersonObjects.First().Family.ShareFamilies.First().Share.Paid_Dues)
            {
                ModelState.AddModelError("CheckinPeople", "Member dues have not been paid!  Please see an employee");
            }
            if (!checkinPersonObjects.Any(p=>!p.Is_Guest.HasValue || !p.Is_Guest.Value))
            {
                ModelState.AddModelError("CheckinPeople", "You must select a member to check in");
            }
            if (ModelState.IsValid)
            {
                return View("ConfirmCheckin", checkinPersonObjects);
            }
            return Search(Id.ToString());
        }

        [HttpPost]
        public ActionResult ConfirmCheckin(int[] CheckinPeople, String[] PersonEmail, int TicketCount, int CashCount)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            var checkinPersonObjects = new List<Person>();
            foreach (var pid in CheckinPeople)
            {
                checkinPersonObjects.Add(ctx.People.Single(p => p.Id == pid));
            }
            if((TicketCount + CashCount) != checkinPersonObjects.Count(p => p.Is_Guest == true)){
                ModelState.AddModelError("CheckinPeople","Cash and ticket counts do not equal guest count");
            }
            if (!checkinPersonObjects.First().Family.ShareFamilies.First().Share.Paid_Dues)
            {
                ModelState.AddModelError("CheckinPeople", "Member dues have not been paid!  Please see an employee");
            }

            var remainingCash = CashCount;
            for (int i = 0; i < CheckinPeople.Length; i++)
            {
                var personId = CheckinPeople[i];
                Person person = ctx.People.Single(p => p.Id == personId);

                var entry = new Entry()
                {
                    Entry_Person = personId,
                    Time = DateTime.Now
                };
                if (person.Is_Guest == true)
                {
                    entry.Entry_Type = remainingCash > 0 ? "CASH" : "TICKET";
                    remainingCash--;
                    AuditLog log = new AuditLog()
                    {
                        date = new DateTime(),
                        message = person.FullName + " used " + entry.Entry_Type,
                        personId = personId
                    };
                    ctx.AuditLogs.Add(log);
                }
                else
                {
                    entry.Entry_Type = "MEMBER";
                }
                ctx.Entries.Add(entry);
                try
                {
                    if (!String.IsNullOrWhiteSpace(PersonEmail[i]))
                    {
                        MailAddress address = new MailAddress(PersonEmail[i]);
                        person.Email = PersonEmail[i];
                    }
                }
                catch (Exception) 
                { }
            }
            if (ModelState.IsValid)
            {

                ctx.SaveChanges();
                TempData["success"] = "Successfully checked in";
                return RedirectToAction("Index");
            }
            return View("ConfirmCheckin", checkinPersonObjects);
        }

        [HttpPost]
        public ActionResult CheckinGuests(int Id, int[] CheckinPeople)
        {
            if (CheckinPeople == null || CheckinPeople.Length == 0)
            {
                return Search(Id.ToString());
            } 
            var viewModel = new GuestCheckinViewModel();
            viewModel.Id = Id;
            viewModel.PersonIdList = CheckinPeople;
            return View("AddGuest", viewModel);
        }

        [HttpPost]
        public ActionResult AddCheckinGuest(int Id, int[] CheckinPeople, Person Person, HttpPostedFileBase picture)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            var refPersonId = CheckinPeople[0];
            var refPerson = ctx.People.Single(p => p.Id == refPersonId);
            if(!Person.Is_Guest.HasValue){
                Person.Is_Guest = true;
            }
            Person.Family = refPerson.Family;
            var peopleList = CheckinPeople.ToList();
            if (picture != null && picture.ContentLength > 0)
            {
                try{
                    var img = Image.FromStream(picture.InputStream);
                    if (img.Height > 200 || img.Width > 200)
                    {
                        Size size = new Size();
                        if(img.Size.Height > img.Size.Width){
                            size.Height = 200;
                            size.Width = (int)(img.Size.Width * (200.0 / img.Size.Height));
                        }else{
                            size.Width = 200;
                            size.Height = (int)(img.Size.Height * (200.0 / img.Size.Width));
                        }
                        img = new Bitmap(img, size);
                    }
                    var ms = new System.IO.MemoryStream();
                    img.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
                    Person.Picture = ms.ToArray();
                }catch(Exception e){
                    ModelState.AddModelError("Person.Picture","Unknown error with image " + e.Message);
                }
            }
            var viewModel = new GuestCheckinViewModel();
            if (ModelState.IsValid)
            {
                ctx.People.Add(Person);
                ctx.SaveChanges();
                peopleList.Add(Person.Id);
                viewModel.PersonIdList = peopleList.ToArray();
                viewModel.Person = new Person();
                TempData["success"] = "Added new guest";
                ModelState.Clear();
            }
            else
            {
                viewModel.PersonIdList = CheckinPeople;
                viewModel.Person = Person;
            }
            viewModel.Id = Id;
            if (ModelState.IsValid && Request.Form.AllKeys.Contains("checkin"))
            {
                return Checkin(Id, viewModel.PersonIdList);
            }else{
                return View("AddGuest", viewModel);
            }
        }

        [HttpGet]
        public ActionResult AddImageToPerson(int Id, int PersonId)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            var Person = ctx.People.Single(p => p.Id == PersonId);
            return View("AddImageToPerson",Person);
        }

        [HttpPost]
        public ActionResult AddImageToPerson(int Id, int PersonId,HttpPostedFileBase picture){
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            if (picture != null && picture.ContentLength > 0)
            {
                var Person = ctx.People.Single(p => p.Id == PersonId);
                try
                {
                    var img = Image.FromStream(picture.InputStream);
                    if (img.Height > 200 || img.Width > 200)
                    {
                        Size size = new Size();
                        if (img.Size.Height > img.Size.Width)
                        {
                            size.Height = 200;
                            size.Width = (int)(img.Size.Width * (200.0 / img.Size.Height));
                        }
                        else
                        {
                            size.Width = 200;
                            size.Height = (int)(img.Size.Height * (200.0 / img.Size.Width));
                        }
                        img = new Bitmap(img, size);
                    }
                    var ms = new System.IO.MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Person.Picture = ms.ToArray();
                    ctx.SaveChanges();
                    TempData["success"] = "Successfully saved person image";
                    return Search(Id.ToString());
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Picture", "Unknown error with image: " + e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("Picture","Select an image to upload");
            }
            return AddImageToPerson(Id, PersonId);

        }

        [HttpPost]
        public ActionResult ArchivePerson(int? id, int? delete)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            if (!ctx.People.Any(s => s.Id == delete))
            {
                return RedirectToAction("Index");
            }
            var person = ctx.People.Single(s => s.Id == delete);
            var family = person.Family;
            person.Family = null;
            family.People.Remove(person);
            ctx.SaveChanges();
            TempData["success"] = "Successfully archived person";
            return Search(id.ToString());
        }


        [HttpPost]
        public ActionResult ArchiveGuest(int? Id)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            if (!ctx.People.Any(s => s.Id == Id && s.Is_Guest.HasValue && s.Is_Guest.Value))
            {
                return RedirectToAction("Index");
            }
            var person = ctx.People.Single(s => s.Id == Id && s.Is_Guest.HasValue && s.Is_Guest.Value);
            var family = person.Family;
            person.Family = null;
            family.People.Remove(person);
            ctx.SaveChanges();
            TempData["success"] = "Successfully archived guest";
            return new JsonResult() { Data = new { Success = true } };
        }

        public ActionResult PersonImage(int? id)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            if (!ctx.People.Any(s => s.Id == id))
            {
                return HttpNotFound();
            }
            var person = ctx.People.Single(s => s.Id == id);
            //var ms = new System.IO.MemoryStream(person.Picture);
            //var img = Image.FromStream(ms);
            
            return new FileContentResult(person.Picture, "image/jpeg");
        }

        [HttpGet]
        public ActionResult EditFamilyGuestList(int? id)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            if (!ctx.Families.Any(f => f.Id == id))
            {
                return RedirectToAction("Index");
            }
            return View(ctx.Families.Single(f => f.Id == id));
        }
    }
}
