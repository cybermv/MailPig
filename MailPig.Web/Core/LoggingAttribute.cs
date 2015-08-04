namespace MailPig.Web.Core
{
    using DAL.Logging;
    using System.Web.Mvc;

    public class LoggingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SimpleLogger.Log(this.GetType().Name, string.Format("Action {0} on controller {1} started execution",
                filterContext.ActionDescriptor.ActionName,
                filterContext.Controller.GetType().Name));

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null && filterContext.HttpContext.Response.StatusCode < 400)
            {
                SimpleLogger.Log(this.GetType().Name, string.Format("Action {0} on controller {1} executed successfully",
                    filterContext.ActionDescriptor.ActionName,
                    filterContext.Controller.GetType().Name));
            }
            else if (filterContext.Exception != null)
            {
                SimpleLogger.Log(this.GetType().Name, string.Format("Action {0} on controller {1} failed with the following exception: {2}",
                    filterContext.ActionDescriptor.ActionName,
                    filterContext.Controller.GetType().Name,
                    filterContext.Exception));
            }
            else
            {
                SimpleLogger.Log(this.GetType().Name, string.Format("Action {0} on controller {1} failed with the following status code: {2}",
                    filterContext.ActionDescriptor.ActionName,
                    filterContext.Controller.GetType().Name,
                    filterContext.HttpContext.Response.StatusCode));
            }

            base.OnActionExecuted(filterContext);
        }
    }
}