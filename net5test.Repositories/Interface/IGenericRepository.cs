using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Repositories.Interface
{
    /// <summary>
    /// Interface Repository
    /// </summary>
    public interface IGenericRepository<TEntity>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">實體</param>
        int Create(TEntity entity);

        /// <summary>
        /// 取得全部
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 取得單筆
        /// </summary>
        /// <param name="predicate">查詢條件</param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity">實體</param>
        void Remove(TEntity entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">實體</param>
        void Update(TEntity entity);
    }
}
