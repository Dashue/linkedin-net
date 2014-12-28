using System.Web;
using System.Web.Mvc;

namespace LinkedIn.Api.Client.Samples.ASPNetMvc5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
