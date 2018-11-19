using System.Web;
using System.Web.Mvc;

namespace NightOwl.PersonRecognitionService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
