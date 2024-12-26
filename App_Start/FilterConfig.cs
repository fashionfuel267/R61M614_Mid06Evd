using System.Web;
using System.Web.Mvc;

namespace R61M614_Mid06Evd
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
