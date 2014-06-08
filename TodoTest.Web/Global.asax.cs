using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TodoTest.Web.Models;

namespace TodoTest.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    [assembly: log4net.Config.XmlConfigurator(Watch = true)]
    public class MvcApplication : System.Web.HttpApplication
    {
        public static List<Todo> TodoList { get; set; }
        public static List<Todo> PaginatorList { get; set; }
        protected void Application_Start()
        {
            TodoList = new List<Todo>();
            PaginatorList = new List<Todo>();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}