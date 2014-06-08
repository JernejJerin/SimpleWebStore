using System.Web;
using System.Web.Mvc;

namespace WebStore
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // we will by default protect all methods of the application
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());

            // application only allows https
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
