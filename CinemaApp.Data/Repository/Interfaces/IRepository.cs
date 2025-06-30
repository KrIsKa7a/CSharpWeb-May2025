namespace CinemaApp.Data.Repository.Interfaces
{
    using System.Linq.Expressions;

    public interface IRepository<TEntity, TKey>
    {
        TEntity? GetById(TKey id);

        TEntity? SingleOrDefault(Func<TEntity, bool> predicate);

        TEntity? FirstOrDefault(Func<TEntity, bool> predicate);

        IEnumerable<TEntity> GetAll();

        int Count();

        IQueryable<TEntity> GetAllAttached();

        void Add(TEntity item);

        void AddRange(IEnumerable<TEntity> items);

        bool Delete(TEntity entity);

        bool HardDelete(TEntity entity);

        bool Update(TEntity item);

        void SaveChanges();
    }
}
