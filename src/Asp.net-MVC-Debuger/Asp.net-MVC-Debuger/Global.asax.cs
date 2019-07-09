using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace Asp.net_MVC_Debuger
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            //ViewEngines.Engines.Add();
            // ControllerBuilder.Current.SetControllerFactory(new MyControllerFactory());
        }
    }

    //public class CoustmerViewEngine : IViewEngine
    //{
    //    public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
    //    {
            
    //    }

    //    public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
    //    {
            
    //    }

    //    public void ReleaseView(ControllerContext controllerContext, IView view)
    //    {
            
    //    }
    //}
}
