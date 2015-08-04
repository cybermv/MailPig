namespace MailPig.DAL.Core
{
    using Model.Core;
    using System.Data.Entity;
    using System.Linq;

    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        private DbContext _context;
        private UnitOfWork _owner;

        public GenericRepository(DbContext context)
            : this(context, null)
        {
        }

        public GenericRepository(DbContext context, UnitOfWork owner)
        {
            this._context = context;
            this._owner = owner;
        }

        public IUnitOfWork Owner { get { return this._owner; } }

        public IQueryable<TEntity> Query { get { return _context.Set<TEntity>(); } }

        public TEntity FindById(int id)
        {
            return this._context.Set<TEntity>().Find(id);
        }

        public TEntity Insert(TEntity entity)
        {
            this._context.Set<TEntity>().Add(entity);
            return this.SaveChanges() > 0 ? entity : null;
        }

        public TEntity Update(TEntity entity)
        {
            this._context.Set<TEntity>().Attach(entity);
            this._context.Entry(entity).State = EntityState.Modified;
            return this.SaveChanges() > 0 ? entity : null;
        }

        public bool Delete(int id)
        {
            TEntity toDelete = FindById(id);

            if (toDelete != null)
            {
                _context.Set<TEntity>().Remove(toDelete);
                return SaveChanges() > 0;
            }
            return false;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public UnitOfWork GetOwner()
        {
            return this._owner;
        }
    }
}