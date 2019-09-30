using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Asp.net_MVC_Debuger.Models;

namespace Asp.net_MVC_Debuger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(MessageViewModel view)
        {
            return View();
        }

        public void TestVoide(int? i,char? b) { }

        public ActionResult HttpHandles()
        {
            
               HttpHandlersSection section = GetConfigSetting<HttpHandlersSection>("HttpHandlers");

            var httpHandlers =
                from h in section.Handlers.Cast<HttpHandlerAction>()
                let action =
                    $"path=\"{h.Path}\" verb=\"{h.Verb}\" validate=\"{h.Validate}\" type=\"{h.Type}\""
                select action;

            return View("HttpView", httpHandlers);
        }

        public ActionResult HttpModules()
        {
            HttpModulesSection section = GetConfigSetting<HttpModulesSection>("HttpModules");

            var httpModules =
                from h in section.Modules.Cast<HttpModuleAction>()
                let action =
                    $"path=\"{h.Name}\" type=\"{h.Type}\""
                select action;

            return View("HttpView", httpModules);
        }


        private T GetConfigSetting<T>(string name)
            where T : ConfigurationSection
        {
            string typeName = typeof(HttpRequest).AssemblyQualifiedName
                .Replace("HttpRequest", "Configuration.RuntimeConfig");
            Type type = Type.GetType(typeName);

            bool useAppConfig = Request.QueryString["useAppConfig"] == "1";

            object config = null;

            if (useAppConfig)
                config = type.InvokeMember("GetAppConfig",
                    BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic,
                    null, null, null);
            else
                config = type.InvokeMember("GetConfig",
                    BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic,
                    null, null, new object[] {System.Web.HttpContext.Current});

            return type.InvokeMember(name,
                BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, config, null) as T; 
        }

        public ActionResult PipelineEvents()
        {
            var eventNames = typeof(HttpApplication)
                .GetEvents().Select(x => x.Name);
            return View(eventNames);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
          
            return View();
        }

        public ActionResult Contact()
        {
            
            ViewBag.Message = "Your contact page.";
           
            return View();
        }
    }
}