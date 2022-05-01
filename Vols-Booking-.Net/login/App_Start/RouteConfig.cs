﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace login
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{id2}/{id3}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional , id2 = UrlParameter.Optional, id3 = UrlParameter.Optional }
            );
        }
    }
}
