using System.Web;
using System.Web.Mvc;

namespace Dr4rum_eProjectIII
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
