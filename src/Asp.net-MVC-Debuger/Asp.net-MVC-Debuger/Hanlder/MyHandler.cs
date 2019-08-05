using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            
            context.Response.Write("Hello World");

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