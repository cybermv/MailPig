namespace MailPig.BL.Core
{
    using DAL.Core;

    public abstract class ServiceBase : IService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            this.UnitOfWork.Dispose();
        }
    }
}