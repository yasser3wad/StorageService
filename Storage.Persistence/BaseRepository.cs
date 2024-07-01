using Storage.CoreInfrastructure.CoreDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Storage.Persistence
{
    internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly StorageContext _context;
        private readonly DbSet<TEntity> _dbSet;


        internal BaseRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<StorageContext>();
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(params object[] keys)
        {
            return await _dbSet.FindAsync(keys);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }


        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate is null)
            {
                predicate = p => true;
            }
            var query = _dbSet.Where(predicate);
            return query;
        }
        public Task<List<TResult>> ToListAsync<TResult>(IQueryable<TResult> query, CancellationToken cancellationToken = default)
        {
            return query.ToListAsync(cancellationToken);
        }
        public void Delete(TEntity entity)
        {

            _dbSet.Remove(entity);

        }
    }
}
