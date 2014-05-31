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
    public class FamilyController : Controller
    {
        private PoolDataEntitiesConnection db = new PoolDataEntitiesConnection();

        //
        // GET: /Admin/Family/

        public ActionResult Index()
        {
            var families = db.Families.Include(f => f.Address).Include(f => f.ShareUserType);
            return View(families.ToList());
        }

        //
        // GET: /Admin/Family/Details/5

        public ActionResult Details(int id = 0)
        {
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        //
        // GET: /Admin/Family/Create

        public ActionResult Create()
        {
            ViewBag.Family_Address = new SelectList(db.Addresses, "Id", "Address2");
            ViewBag.Family_ShareUserType = new SelectList(db.ShareUserTypes, "Id", "Name");
            return View();
        }

        //
        // POST: /Admin/Family/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Family family)
        {
            if (ModelState.IsValid)
            {
                db.Families.Add(family);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Family_Address = new SelectList(db.Addresses, "Id", "Address2", family.Family_Address);
            ViewBag.Family_ShareUserType = new SelectList(db.ShareUserTypes, "Id", "Name", family.Family_ShareUserType);
            return View(family);
        }

        //
        // GET: /Admin/Family/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            ViewBag.Family_Address = new SelectList(db.Addresses, "Id", "Address2", family.Family_Address);
            ViewBag.Family_ShareUserType = new SelectList(db.ShareUserTypes, "Id", "Name", family.Family_ShareUserType);
            return View(family);
        }

        //
        // POST: /Admin/Family/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Family family)
        {
            if (ModelState.IsValid)
            {
                db.Entry(family).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Family_Address = new SelectList(db.Addresses, "Id", "Address2", family.Family_Address);
            ViewBag.Family_ShareUserType = new SelectList(db.ShareUserTypes, "Id", "Name", family.Family_ShareUserType);
            return View(family);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}