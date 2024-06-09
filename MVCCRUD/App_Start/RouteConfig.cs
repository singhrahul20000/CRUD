using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCCRUD
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "Profile/LoginUser",
                defaults: new { controller = "Profile", action = "LoginUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Register",
                url: "Profile/RegisterUser",
                defaults: new { controller = "Profile", action = "RegisterUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "User Home",
                url: "Home/Index",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Home",
                url: "Profile/Index",
                defaults: new { controller = "Profile", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Profile", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
