using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Asp.net_MVC_Debuger.Hanlder;

namespace Asp.net_MVC_Debuger
{
    public class MyRouteProvider : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MyHandler();
        }
    }

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute(
                "PhysicalFile",
                "GetFile/{Name}",
                "~/PhysicalFile.aspx", true,
                new RouteValueDictionary()
                {
                    { "Name","PhysicalFile"}
                });

            routes.Add(new Route("Customer",new MyHandlerRouter()));
    		routes.Add("Test",new Route("{controller}/Test123",
                new MyRouteProvider())
            {
                Defaults = new RouteValueDictionary() { { "controller" , "Home"} }
            });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           
        }
    }
}
