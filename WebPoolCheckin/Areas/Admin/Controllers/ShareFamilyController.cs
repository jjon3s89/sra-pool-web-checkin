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
    public class ShareFamilyController : Controller
    {
        private PoolDataEntitiesConnection db = new PoolDataEntitiesConnection();

        //
        // GET: /Admin/ShareFamily/

        public ActionResult Index()
        {
            var sharefamilies = db.ShareFamilies.Include(s => s.Family).Include(s => s.Share).Include(s => s.ShareUserType);
            return View(sharefamilies.ToList());
        }

        //
        // GET: /Admin/ShareFamily/Details/5

        public ActionResult Details(int id = 0)
        {
            ShareFamily sharefamily = db.ShareFamilies.Find(id);
            if (sharefamily == null)
            {
                return HttpNotFound();
            }
            return View(sharefamily);
        }

        //
        // GET: /Admin/ShareFamily/Create

        public ActionResult Create()
        {
            ViewBag.ShareFamily_Family = new SelectList(db.Families, "Id", "FamilyName");
            ViewBag.ShareFamily_Share = new SelectList(db.Shares, "Id", "Id");
            ViewBag.ShareFamily_ShareUserType = new SelectList(db.ShareUserTypes, "Id", "Name");
            return View();
        }

        //
        // POST: /Admin/ShareFamily/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShareFamily sharefamily)
        {
            if (ModelState.IsValid)
            {
                db.ShareFamilies.Add(sharefamily);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShareFamily_Family = new SelectList(db.Families, "Id", "FamilyName", sharefamily.ShareFamily_Family);
            ViewBag.ShareFamily_Share = new SelectList(db.Shares, "Id", "Id", sharefamily.ShareFamily_Share);
            ViewBag.ShareFamily_ShareUserType = new SelectList(db.ShareUserTypes, "Id", "Name", sharefamily.ShareFamily_ShareUserType);
            return View(sharefamily);
        }

        //
        // GET: /Admin/ShareFamily/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ShareFamily sharefamily = db.ShareFamilies.Find(id);
            if (sharefamily == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShareFamily_Family = new SelectList(db.Families, "Id", "FamilyName", sharefamily.ShareFamily_Family);
            ViewBag.ShareFamily_Share = new SelectList(db.Shares, "Id", "Id", sharefamily.ShareFamily_Share);
            ViewBag.ShareFamily_ShareUserType = new SelectList(db.ShareUserTypes, "Id", "Name", sharefamily.ShareFamily_ShareUserType);
            return View(sharefamily);
        }

        //
        // POST: /Admin/ShareFamily/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShareFamily sharefamily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sharefamily).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShareFamily_Family = new SelectList(db.Families, "Id", "FamilyName", sharefamily.ShareFamily_Family);
            ViewBag.ShareFamily_Share = new SelectList(db.Shares, "Id", "Id", sharefamily.ShareFamily_Share);
            ViewBag.ShareFamily_ShareUserType = new SelectList(db.ShareUserTypes, "Id", "Name", sharefamily.ShareFamily_ShareUserType);
            return View(sharefamily);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}