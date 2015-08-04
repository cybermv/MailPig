namespace MailPig.Web.Core
{
    using System.Web.Mvc;

    public class ErrorCatcherAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("/Error?code=500");
            filterContext.ExceptionHandled = true;
        }
    }
}