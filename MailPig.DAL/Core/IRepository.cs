namespace MailPig.DAL.Core
{
    using System.Linq;

    public interface IRepository<TEntity>
    {
        IUnitOfWork Owner { get; }

        IQueryable<TEntity> Query { get; }

        TEntity FindById(int id);

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        bool Delete(int id);

        int SaveChanges();
    }
}