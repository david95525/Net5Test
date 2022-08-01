using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net5test.Repositories.Interface;
using System.Linq.Expressions;
using net5test.Repositories.DataModels;
using Microsoft.EntityFrameworkCore;

namespace net5test.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// UnitOfWork 實體
        /// </summary>
        private readonly DbContext _context;
        public GenericRepository(DbContext context)
        {
            this._context = context;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">實體</param>
        public int Create(TEntity entity)
        {
              try
            {
                _context.Set<TEntity>().Add(entity);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("entity");
            }
        }

        /// <summary>
        /// 取得全部
        /// </summary>
        /// <returns></returns>
        public  IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// 取得單筆
        /// </summary>
        /// <param name="predicate">查詢條件</param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            
            return  _context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity">實體</param>
        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this._context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">實體</param>
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this._context.Entry(entity).State = EntityState.Modified;
        }
    }
}
