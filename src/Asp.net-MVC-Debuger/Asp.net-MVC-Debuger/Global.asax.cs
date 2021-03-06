﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using Asp.net_MVC_Debuger.Controllers;
using Asp.net_MVC_Debuger.Core;

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

            ModelBinders.Binders.Add(typeof(ModelBindDemo),new FooModelBinder());
            //ViewEngines.Engines.Add();
            //ControllerBuilder.Current.SetControllerFactory(new ReflectionControllerFactory());
        }
    }
}
