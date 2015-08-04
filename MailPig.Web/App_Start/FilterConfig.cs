using System.Web.Mvc;

namespace MailPig.Web
{
    using Core;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorCatcherAttribute());
        }
    }
}