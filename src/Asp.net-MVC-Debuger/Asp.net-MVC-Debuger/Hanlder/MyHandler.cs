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
}