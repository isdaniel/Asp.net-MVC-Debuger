using System.Web;
using System.Web.Mvc;

namespace Asp.net_MVC_Debuger
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
