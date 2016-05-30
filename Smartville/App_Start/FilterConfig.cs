using Smartville.Filters;
using System.Web;
using System.Web.Mvc;

namespace Smartville
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserInfoFilter());
        }
    }
}
