using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPoolCheckin.Models;

namespace WebPoolCheckin.Areas.Report.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/Report/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hourly()
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            return View(ctx.hourlycounts.OrderByDescending(h => h.entry_date).ThenBy(h => h.hour_slot));
            //return View(ctx.hourlycounts);
        }
        public ActionResult EmployeeTime()
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            return View(ctx.rpt_employee_time.OrderBy(h => h.LastName).ThenBy(h => h.date_time));
        }
        public ActionResult Emails()
        {
            PoolDataEntitiesConnection ctx = new PoolDataEntitiesConnection();
            return View(ctx.People.Where(p=>!(p.Email == null || p.Email == "")).OrderBy(h => h.LastName).ThenBy(h => h.Email));
        }
    }
}
