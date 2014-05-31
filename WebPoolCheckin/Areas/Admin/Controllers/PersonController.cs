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
    public class PersonController : Controller
    {
        private PoolDataEntitiesConnection db = new PoolDataEntitiesConnection();

        //
        // GET: /Admin/Person/

        public ActionResult Index(string filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var filterFirst = filter;
                var filterLast = filter;
                IEnumerable<Person> people;
                if (filter.Trim().Contains(' '))
                {
                    filterFirst = filter.Split(' ')[0];
                    filterLast = filter.Split(' ')[1];
                    //first AND last
                    people = from p in db.People where p.FirstName.Contains(filterFirst) && p.LastName.Contains(filterLast) orderby p.LastName, p.FirstName select p;
                }
                else
                {
                    //first OR last
                    people = from p in db.People where p.FirstName.Contains(filterFirst) || p.LastName.Contains(filterLast) orderby p.LastName, p.FirstName select p;
                }
                if (people.Count() > 30)
                {
                    TempData["error"] = "More than 30 people found.  Limiting view to First 30.";
                }
                return View(people.Take(30));
            }
            return View(new List<Person>());
        }

        //
        // GET: /Admin/Person/Details/5

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
        // GET: /Admin/Person/Create

        public ActionResult Create()
        {
            ViewBag.Family_Person = new SelectList(db.Families, "Id", "NameAndShare");
            ViewBag.FamilyMemberType_Person = new SelectList(db.FamilyMemberTypes, "Id", "Name");
            return View();
        }

        //
        // POST: /Admin/Person/Create

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

            ViewBag.Family_Person = new SelectList(db.Families, "Id", "NameAndShare", person.Family_Person);
            ViewBag.FamilyMemberType_Person = new SelectList(db.FamilyMemberTypes, "Id", "Name", person.FamilyMemberType_Person);
            return View(person);
        }

        //
        // GET: /Admin/Person/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.Family_Person = new SelectList(db.Families, "Id", "NameAndShare", person.Family_Person);
            ViewBag.FamilyMemberType_Person = new SelectList(db.FamilyMemberTypes, "Id", "Name", person.FamilyMemberType_Person);
            return View(person);
        }

        //
        // POST: /Admin/Person/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, Person person)
        {
            person.Id = Id;
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Successfully saved person!";
                return RedirectToAction("Index");
            }
            ViewBag.Family_Person = new SelectList(db.Families, "Id", "FamilyName", person.Family_Person);
            ViewBag.FamilyMemberType_Person = new SelectList(db.FamilyMemberTypes, "Id", "Name", person.FamilyMemberType_Person);
            return View(person);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}