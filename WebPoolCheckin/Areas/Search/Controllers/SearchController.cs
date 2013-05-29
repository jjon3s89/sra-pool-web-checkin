using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPoolCheckin.Models;
using WebPoolCheckin.Areas.Search.Models;

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
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            if(!ctx.Shares.Any(s=>s.Id==shareId && s.Active==true)){
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
                return Search(Id.ToString());
            }
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            var checkinPersonObjects = new List<Person>();
            foreach (var pid in CheckinPeople)
            {
                checkinPersonObjects.Add(ctx.People.Single(p => p.Id == pid));
            }
            return View("ConfirmCheckin",checkinPersonObjects);
        }

        [HttpPost]
        public ActionResult ConfirmCheckin(int[] CheckinPeople)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            foreach (var personid in CheckinPeople)
            {
                var entry = new Entry()
                {
                    Entry_Person = personid,
                    Time = DateTime.Now
                };
                ctx.Entries.Add(entry);
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");
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
                    if (img.Height > 400 || img.Width > 400)
                    {
                        Size size = new Size();
                        if(img.Size.Height > img.Size.Width){
                            size.Height = 400;
                            size.Width = (int)(img.Size.Width * (200.0 / img.Size.Height));
                        }else{
                            size.Width = 400;
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
                    if (img.Height > 400 || img.Width > 400)
                    {
                        Size size = new Size();
                        if (img.Size.Height > img.Size.Width)
                        {
                            size.Height = 400;
                            size.Width = (int)(img.Size.Width * (200.0 / img.Size.Height));
                        }
                        else
                        {
                            size.Width = 400;
                            size.Height = (int)(img.Size.Height * (200.0 / img.Size.Width));
                        }
                        img = new Bitmap(img, size);
                    }
                    var ms = new System.IO.MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Person.Picture = ms.ToArray();
                    ctx.SaveChanges();
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

        public ActionResult PersonImage(int? id)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            if (!ctx.People.Any(s => s.Id == id))
            {
                return null;
            }
            var person = ctx.People.Single(s => s.Id == id);
            //var ms = new System.IO.MemoryStream(person.Picture);
            //var img = Image.FromStream(ms);
            
            return new FileContentResult(person.Picture, "image/jpeg");
        }
    }
}
