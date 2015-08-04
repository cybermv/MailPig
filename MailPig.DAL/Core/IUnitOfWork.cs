namespace MailPig.DAL.Core
{
    using Model.Core;
    using System;
    using System.Data.Entity;

    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;

        void Commit();

        void Rollback();
    }
}