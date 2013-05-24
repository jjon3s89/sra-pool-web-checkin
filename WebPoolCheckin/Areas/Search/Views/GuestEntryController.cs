using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPoolCheckin.Models;

namespace WebPoolCheckin.Areas.Search.Views
{
    public class GuestEntryController : Controller
    {
        private PoolDataEntitiesConnection db = new PoolDataEntitiesConnection();

        //
        // GET: /Search/GuestEntry/

        public ActionResult Index()
        {
            var people = db.People.Include(p => p.Family).Include(p => p.FamilyMemberType);
            return View(people.ToList());
        }

        //
        // GET: /Search/GuestEntry/Details/5

        public ActionResult Details(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // GET: /Search/GuestEntry/Create

        public ActionResult Create()
        {
            ViewBag.Family_Person = new SelectList(db.Families, "Id", "FamilyName");
            ViewBag.FamilyMemberType_Person = new SelectList(db.FamilyMemberTypes, "Id", "Name");
            return View();
        }

        //
        // POST: /Search/GuestEntry/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Family_Person = new SelectList(db.Families, "Id", "FamilyName", person.Family_Person);
            ViewBag.FamilyMemberType_Person = new SelectList(db.FamilyMemberTypes, "Id", "Name", person.FamilyMemberType_Person);
            return View(person);
        }

        //
        // GET: /Search/GuestEntry/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.Family_Person = new SelectList(db.Families, "Id", "FamilyName", person.Family_Person);
            ViewBag.FamilyMemberType_Person = new SelectList(db.FamilyMemberTypes, "Id", "Name", person.FamilyMemberType_Person);
            return View(person);
        }

        //
        // POST: /Search/GuestEntry/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Family_Person = new SelectList(db.Families, "Id", "FamilyName", person.Family_Person);
            ViewBag.FamilyMemberType_Person = new SelectList(db.FamilyMemberTypes, "Id", "Name", person.FamilyMemberType_Person);
            return View(person);
        }

        //
        // GET: /Search/GuestEntry/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /Search/GuestEntry/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}