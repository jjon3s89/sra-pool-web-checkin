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
        [HttpPost]
        public ActionResult Search(String searchText)
        {
            int shareId;
            if (!Int32.TryParse(searchText, out shareId))
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
            if (activeShareFamilies.SelectMany(sf=>sf.Family.People).Any(p=>p.Picture == null ))
            {

            }
            return View("Search",share.Single());
        }
        public ActionResult Checkin(int Id)
        {
                return Search(Id.ToString());
        }

        [HttpPost]
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
            var viewModel = new GuestCheckinViewModel();
            viewModel.PersonIdList = CheckinPeople;
            return View("AddGuest", viewModel);
        }

        [HttpPost]
        public ActionResult AddCheckinGuest(int[] CheckinPeople, Person Person)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            var refPersonId = CheckinPeople[0];
            var refPerson = ctx.People.Single(p => p.Id == refPersonId);
            Person.Is_Guest = true;
            Person.Family = refPerson.Family;
            var peopleList = CheckinPeople.ToList();
            ctx.People.Add(Person);
            ctx.SaveChanges();
            peopleList.Add(Person.Id);
            var viewModel = new GuestCheckinViewModel();
            viewModel.PersonIdList = peopleList.ToArray();
            return View("AddGuest", viewModel);
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
