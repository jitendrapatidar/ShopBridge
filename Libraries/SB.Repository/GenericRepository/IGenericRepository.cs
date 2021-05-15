using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SB.Repository.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region "Async Methods"

        #region "Get"

        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<ICollection<TEntity>> GetManyQueryableAsync(Expression<Func<TEntity, bool>> where);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetSingleAsync(Func<TEntity, bool> predicate);

        #endregion

        #region "Insert"

        Task<TEntity> InsertAsync(TEntity entity);

        Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entity);

        #endregion

        #region "Delete"

        Task<int> DeleteAsync(object id);

        Task<int> DeleteAsync(TEntity t);

        #endregion

        #region "Update"

        Task<TEntity> UpdateAsync(TEntity entityToUpdate);

        Task<TEntity> UpdateAsync(TEntity updated, int key);

        #endregion

        #region "Query and Procedure"

        //void QueryContextExtAsync(string query);

        #endregion

        #endregion

    }
}
