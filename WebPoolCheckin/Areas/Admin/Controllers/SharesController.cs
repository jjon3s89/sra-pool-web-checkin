using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPoolCheckin.Models;

namespace WebPoolCheckin.Areas.Admin.Controllers
{
    public class SharesController : Controller
    {
        private PoolDataEntitiesConnection db = new PoolDataEntitiesConnection();

        //
        // GET: /Admin/Shares/

        public ActionResult Index()
        {
            var shares = db.Shares.Include(s => s.Address);
            return View(shares.ToList());
        }

        //
        // GET: /Admin/Shares/Details/5

        public ActionResult Details(int id = 0)
        {
            Share share = db.Shares.Find(id);
            if (share == null)
            {
                return HttpNotFound();
            }
            return View(share);
        }

        //
        // GET: /Admin/Shares/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Share share = db.Shares.Find(id);
            if (share == null)
            {
                return HttpNotFound();
            }
            ViewBag.Share_Address = new SelectList(db.Addresses, "Id", "Address2", share.Share_Address);
            return View(share);
        }

        //
        // POST: /Admin/Shares/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Share share)
        {
            if (ModelState.IsValid)
            {
                db.Entry(share).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Share_Address = new SelectList(db.Addresses, "Id", "Address2", share.Share_Address);
            return View(share);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}