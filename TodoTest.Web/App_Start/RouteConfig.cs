using System.Web.Mvc;
using System.Web.Routing;

namespace TodoTest.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "List",
                url: "Todo/List/{page}/{pageSize}",
                defaults: new { controller = "Todo", action = "List", page = UrlParameter.Optional, pageSize = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Remove",
                url: "Todo/Remove/{guid}",
                defaults: new { controller = "Todo", action = "Remove" }
            );

            routes.MapRoute(
                name: "Add",
                url: "Todo/Add/{name}",
                defaults: new { controller = "Todo", action = "Add" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}