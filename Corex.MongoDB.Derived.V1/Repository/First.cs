using Corex.MongoDB.Inftrastructure;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Corex.MongoDB.Derived.V1.Repository
{
    public partial class Repository<T> where T : class, IMongoModel
    {
        #region First

        /// <summary>
        /// get first item in collection
        /// </summary>
        /// <returns>entity of <typeparamref name="T"/></returns>
        public T First()
        {
            return FindAll(i => i.Id, 0, 1, false).FirstOrDefault();
        }

        /// <summary>
        /// FirstOrDefaultAsync does not exist on regular list so this is special case
        /// </summary>
        /// <returns></returns>
        public async Task<T> FirstAsync()
        {
            return await Retry(async () =>
            {
                var query = Query().Skip(0 * 1).Limit(1);
                return await query.SortBy(i => i.Id).FirstOrDefaultAsync();
            });
        }

        /// <summary>
        /// get first item in query
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>entity of <typeparamref name="T"/></returns>
        public T First(Expression<Func<T, bool>> filter)
        {
            return First(filter, i => i.Id);
        }

        /// <summary>
        /// get first item in query
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>entity of <typeparamref name="T"/></returns>
        public async Task<T> FirstAsync(Expression<Func<T, bool>> filter)
        {
            return await FirstAsync(filter, i => i.Id);
        }
        /// <summary>
        /// get first item in query with order
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <returns>entity of <typeparamref name="T"/></returns>
        public T First(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order)
        {
            return First(filter, order, false);
        }
        /// <summary>
        /// get first item in query with order
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <returns>entity of <typeparamref name="T"/></returns>
        public async Task<T> FirstAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order)
        {
            return await FirstAsync(filter, order, false);
        }

        /// <summary>
        /// get first item in query with order and direction
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="isDescending">ordering direction</param>
        /// <returns>entity of <typeparamref name="T"/></returns>
        public T First(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order, bool isDescending)
        {
            return Find(filter, order, 0, 1, isDescending).SingleOrDefault();
        }

        /// <summary>
        /// get first item in query with order and direction
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="isDescending">ordering direction</param>
        /// <returns>entity of <typeparamref name="T"/></returns>
        public async Task<T> FirstAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order, bool isDescending)
        {
            return await Retry(async () =>
            {
                var query = Query(filter).Skip(0 * 1).Limit(1);
                return await (isDescending ? query.SortByDescending(order) : query.SortBy(order)).SingleOrDefaultAsync();
            });
        }
        #endregion First

        #region Get

        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id">id value</param>
        /// <returns>entity of <typeparamref name="T"/></returns>
        public virtual T Get(Guid id)
        {
            return Retry(() =>
            {
                return Find(i => i.Id == id).FirstOrDefault();
            });
        }

        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id">id value</param>
        /// <returns>entity of <typeparamref name="T"/></returns>
        public virtual async Task<T> GetAsync(Guid id)
        {
            return await Retry(async () =>
            {
                return await FirstAsync(i => i.Id == id);
            });
        }

        #endregion Get

    }
}
