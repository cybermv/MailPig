namespace MailPig.BL.Core
{
    using DAL.Core;
    using System;

    public interface IService : IDisposable
    {
        IUnitOfWork UnitOfWork { get; set; }
    }
}