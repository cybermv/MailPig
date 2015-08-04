namespace MailPig.Web.Core
{
    using DAL.Core;
    using Ninject;
    using System.Web.Http;

    public abstract class MailPigApiControllerBase : ApiController
    {
        public IUnitOfWork UnitOfWork { get; set; }

        protected MailPigApiControllerBase()
        {
            UnitOfWork = NinjectWebCommon.Kernel.Get<IUnitOfWork>();
        }
    }
}