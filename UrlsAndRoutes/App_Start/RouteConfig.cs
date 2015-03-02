using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        //post attribute routing
        public static void RegisterRoutes(RouteCollection routes)
        {
            ////attribute routing is off by default, needs this in place to enable it.
            //routes.MapMvcAttributeRoutes();

            routes.MapRoute("NewRoute", "App/Do{action}",
                new { controller = "Home" });

            routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        //
        //Pre-attribute routing
        //
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    ////Routes must be defined from most specific to least specific or else specific routes will get matched to the more generic routes, and return the wrong controller/action pair.
        //    ////This is re-routing from a past schema. In this one, unlike below, neither the controller nor the action still exist. Both are re-routed.
        //    //routes.MapRoute("ShopSchema2", "Shop/OldAction", new {controller = "Home", action = "Index"});

        //    ////The Shop controller no longer exists and instead we decided to route it to Home with the same action. This is useful for old bookmarks and links, so that they can still get to the site.
        //    //routes.MapRoute("ShopSchema", "Shop/{action}", new {controller = "Home"});

        //    ////Mixing static and segment. Will match URL's in the form of XHome/Index
        //    //routes.MapRoute("", "X{controller}/{action}");

        //    //routes.MapRoute("MyRoute", "{controller}/{action}", new { controller = "Home", action = "Index" });

        //    ////using a static segment like public. Will only match this pattern if the prefix "Public" is in before the other two segments.
        //    //routes.MapRoute("", "Public/{controller}/{action}", new { controller = "Home", action = "Index" });
        //    //
        //    //
        //    //Starting over...

        //    //The below was showing using a required URL parameter, since the default value is set
        //    //routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
        //    //    new { controller = "Home", action = "Index", id = "DefaultId" });

        //    //This one shows an optional parameter, this will match whether or not the value is specified
        //    //routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
        //    //    new { controller = "Home", action = "Index", id = UrlParameter.Optional });

        //    //this one will match any number of segments supplied
        //    routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
        //        new { controller = "Home", action = "Index", id = UrlParameter.Optional },
        //        new
        //        {
        //            controller = "^H.*",
        //            action = "Index|About",
        //            httpMethod = new HttpMethodConstraint("Get"),
        //            id = new CompoundRouteConstraint(new IRouteConstraint[]
        //            {
        //                new AlphaRouteConstraint(),
        //                new MinLengthRouteConstraint(6)
        //            })
        //        },
        //            new[] { "UrlsAndRoutes.Controllers" });
        //}
    }
}