namespace MailPig.Web.Core
{
    using System.Web.Mvc;

    public class UnitOfWorkHandlerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Controller is MailPigControllerBase)
            {
                MailPigControllerBase controller = filterContext.Controller as MailPigControllerBase;

                if (filterContext.Exception == null)
                {
                    controller.UnitOfWork.Commit();
                }
                else
                {
                    controller.UnitOfWork.Rollback();
                }
            }
        }
    }
}