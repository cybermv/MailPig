﻿using System.Web.Mvc;
using System.Web.Routing;

namespace MailPig.Web
{
    using System.Web.Http;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Welcome", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}