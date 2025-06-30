namespace CinemaApp.Data.Repository
{
    using System.Linq.Expressions;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using static Common.ExceptionMessages;
    using static GCommon.ApplicationConstants;

    public abstract class BaseRepository<TEntity, TKey> 
        : IRepository<TEntity, TKey>, IAsyncRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly CinemaAppDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(CinemaAppDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        public TEntity? GetById(TKey id)
        {
            return this.DbSet
                .Find(id);
        }

        public ValueTask<TEntity?> GetByIdAsync(TKey id)
        {
            return this.DbSet
                .FindAsync(id);
        }

        public TEntity? SingleOrDefault(Func<TEntity, bool> predicate)
        {
            return this.DbSet
                .SingleOrDefault(predicate);
        }

        public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet
                .SingleOrDefaultAsync(predicate);
        }

        public TEntity? FirstOrDefault(Func<TEntity, bool> predicate)
        {
            return this.DbSet
                .FirstOrDefault(predicate);
        }

        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.DbSet
                .ToArray();
        }

        public int Count()
        {
            return this.DbSet
                .Count();
        }

        public Task<int> CountAsync()
        {
            return this.DbSet
                .CountAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            TEntity[] entities = await this.DbSet
                .ToArrayAsync();
            
            return entities;
        }

        public IQueryable<TEntity> GetAllAttached()
        {
            return this.DbSet
                .AsQueryable();
        }

        public void Add(TEntity item)
        {
            this.DbSet.Add(item);
            this.DbContext.SaveChanges();
        }

        public async Task AddAsync(TEntity item)
        {
            await this.DbSet.AddAsync(item);
            await this.DbContext.SaveChangesAsync();
        }

        public void AddRange(IEnumerable<TEntity> items)
        {
            this.DbSet.AddRange(items);
            this.DbContext.SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> items)
        {
            await this.DbSet.AddRangeAsync(items);
            await this.DbContext.SaveChangesAsync();
        }

        public bool Delete(TEntity entity)
        {
            this.PerformSoftDeleteOfEntity(entity);

            return this.Update(entity);
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            this.PerformSoftDeleteOfEntity(entity);

            return this.UpdateAsync(entity);
        }

        public bool HardDelete(TEntity entity)
        {
            this.DbSet.Remove(entity);
            int updateCnt = this.DbContext.SaveChanges();

            return updateCnt > 0;
        }

        public async Task<bool> HardDeleteAsync(TEntity entity)
        {
            this.DbSet.Remove(entity);
            int updateCnt = await this.DbContext.SaveChangesAsync();

            return updateCnt > 0;
        }

        public bool Update(TEntity item)
        {
            try
            {
                this.DbSet.Attach(item);
                this.DbSet.Entry(item).State = EntityState.Modified;
                this.DbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity item)
        {
            try
            {
                this.DbSet.Attach(item);
                this.DbSet.Entry(item).State = EntityState.Modified;
                await this.DbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SaveChanges()
        {
            this.DbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await this.DbContext.SaveChangesAsync();
        }

        private void PerformSoftDeleteOfEntity(TEntity entity)
        {
            PropertyInfo? isDeletedProperty = 
                this.GetIsDeletedProperty(entity);
            if (isDeletedProperty == null)
            {
                throw new InvalidOperationException(SoftDeleteOnNonSoftDeletableEntity);
            }

            isDeletedProperty.SetValue(entity, true);
        }

        private PropertyInfo? GetIsDeletedProperty(TEntity entity)
        {
            return typeof(TEntity)
                .GetProperties()
                .FirstOrDefault(pi => pi.PropertyType == typeof(bool) &&
                                                 pi.Name == IsDeletedPropertyName);
        }
    }
}
