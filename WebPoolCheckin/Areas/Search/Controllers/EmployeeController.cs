using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPoolCheckin.Models;

namespace WebPoolCheckin.Areas.Search.Controllers
{
    public class EmployeeController : Controller
    {
        private PoolDataEntitiesConnection db = new PoolDataEntitiesConnection();

        //
        // GET: /Search/Employee/

        public ActionResult Index()
        {
            var employeetimes = db.EmployeeTimes.Include(e => e.Employee);
            return View(employeetimes.ToList());
        }

        public ActionResult Checkin(int id)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            if (!ctx.Employees.Any(e => e.Id == id && e.Active == true)){
                return RedirectToAction("Index", "Search");
            }
            return View(ctx.Employees.Single(e => e.Id == id ));
        }

        [HttpPost]
        public ActionResult EmployeeCheckedIn(int id, bool IsEntry)
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            EmployeeTime checkinTime = new EmployeeTime();
            checkinTime.Time = DateTime.Now;
            checkinTime.Entry = IsEntry;
            checkinTime.EmployeeId = id;
            ctx.EmployeeTimes.Add(checkinTime);
            ctx.SaveChanges();
            TempData["success"] = "Employee checked " + (IsEntry ? "In" : "Out");
            return RedirectToAction("Index", "Search");
        }

        //
        // GET: /Search/Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            EmployeeTime employeetime = db.EmployeeTimes.Find(id);
            if (employeetime == null)
            {
                return HttpNotFound();
            }
            return View(employeetime);
        }

        //
        // GET: /Search/Employee/Create

        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName");
            return View();
        }

        //
        // POST: /Search/Employee/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeTime employeetime)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeTimes.Add(employeetime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeetime.EmployeeId);
            return View(employeetime);
        }

        //
        // GET: /Search/Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EmployeeTime employeetime = db.EmployeeTimes.Find(id);
            if (employeetime == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeetime.EmployeeId);
            return View(employeetime);
        }

        //
        // POST: /Search/Employee/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeTime employeetime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeetime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeetime.EmployeeId);
            return View(employeetime);
        }

        //
        // GET: /Search/Employee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EmployeeTime employeetime = db.EmployeeTimes.Find(id);
            if (employeetime == null)
            {
                return HttpNotFound();
            }
            return View(employeetime);
        }

        //
        // POST: /Search/Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeTime employeetime = db.EmployeeTimes.Find(id);
            db.EmployeeTimes.Remove(employeetime);
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