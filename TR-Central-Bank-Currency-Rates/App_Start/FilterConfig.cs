using System.Web;
using System.Web.Mvc;

namespace TR_Central_Bank_Currency_Rates
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
