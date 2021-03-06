﻿using System.Web.Mvc;

namespace WebPoolCheckin.Areas.Search
{
    public class SearchAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Search";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Search_default",
                "Search/{controller}/{action}/{id}",
                new { controller="Search", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
