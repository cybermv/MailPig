namespace MailPig.DAL.Core
{
    using Model.Core;
    using System;
    using System.Data;
    using System.Data.Entity;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext _context;
        private DbContextTransaction _transaction;

        public UnitOfWork(DbContext context)
        {
            this._context = context;
            StartTransaction();
        }

        public DbContext Context { get { return _context; } }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
        {
            StartTransaction();
            return new GenericRepository<TEntity>(this._context, this);
        }

        public void Commit()
        {
            if (_transaction != null &&
                _transaction.UnderlyingTransaction.Connection != null &&
                _transaction.UnderlyingTransaction.Connection.State != ConnectionState.Closed)
            {
                this._transaction.Commit();
                KillTransaction();
            }
        }

        public void Rollback()
        {
            if (_transaction != null &&
                _transaction.UnderlyingTransaction.Connection != null &&
                _transaction.UnderlyingTransaction.Connection.State != ConnectionState.Closed)
            {
                this._transaction.Rollback();
                KillTransaction();
            }
        }

        public void Dispose()
        {
            if (this._transaction != null)
            {
                Rollback();
                KillTransaction();
            }
            this._context.Dispose();
        }

        private void StartTransaction()
        {
            if (_transaction == null)
            {
                this._transaction = this._context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }
        }

        private void KillTransaction()
        {
            if (this._transaction != null)
            {
                this._transaction.Dispose();
                this._transaction = null;
            }
        }
    }
}