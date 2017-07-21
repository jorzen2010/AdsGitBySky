using System.Web;
using System.Web.Mvc;
using AdsWeb.Attributes;

namespace AdsWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthorizeAttribute());
        }
        
    }
}
