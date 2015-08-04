namespace MailPig.Web.Core
{
    using DAL.Core;
    using System.Net;
    using System.Web.Mvc;

    [Logging]
    [UnitOfWorkHandler]
    public abstract class MailPigControllerBase : Controller
    {
        public IUnitOfWork UnitOfWork { get; set; }

        protected MailPigControllerBase(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing)
        {
            this.UnitOfWork.Dispose();
            base.Dispose(disposing);
        }

        protected ActionResult StatusCode(HttpStatusCode code)
        {
            return new HttpStatusCodeResult(code);
        }
    }
}