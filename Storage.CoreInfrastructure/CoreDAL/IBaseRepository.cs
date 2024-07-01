using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Storage.CoreInfrastructure.CoreDAL
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(params object[] keys);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null ); 
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<List<TResult>> ToListAsync<TResult>(IQueryable<TResult> query, CancellationToken cancellationToken = default);
        void Delete(TEntity entity);

    }
}
