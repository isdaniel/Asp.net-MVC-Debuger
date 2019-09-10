using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Asp.net_MVC_Debuger.Hanlder
{
    public class MyHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
         
            context.Response.Write("Hello MyHandler!!");
        }
    }

    public class MyHandlerRouter : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MyHandler();
        }
    }

    public class MyModuel : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.ResolveRequestCache += Context_PostAcquireRequestState;
            
        }

        private void Context_PostAcquireRequestState(object sender, EventArgs e)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}