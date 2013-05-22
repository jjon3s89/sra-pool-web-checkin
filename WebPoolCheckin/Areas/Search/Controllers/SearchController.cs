using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPoolCheckin.Models;

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
            if(!ctx.Shares.Any(s=>s.Id==shareId)){
                ModelState.AddModelError("ShareText", "Membership not Found");
                return View();
            }
            var share = from s in ctx.Shares where s.Id==shareId select s;
            return View(share.Single());
        }

        [HttpPost]
        public ActionResult Checkin(int Id,int[] CheckinPeople)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            if (!ctx.Shares.Any(s => s.Id == Id))
            {
                ModelState.AddModelError("ShareText", "Membership not Found");
                return View();
            }
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
