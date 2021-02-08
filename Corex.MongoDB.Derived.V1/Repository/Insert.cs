using Corex.MongoDB.Inftrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corex.MongoDB.Derived.V1.Repository
{
    public partial class Repository<T> where T : class, IMongoModel
    {
        #region Insert

        /// <summary>
        /// insert entity
        /// </summary>
        /// <param name="entity">entity</param>
        public virtual void Insert(T entity)
        {
            Retry(() =>
            {
                Collection.InsertOne(entity);
                return true;
            });
        }
        /// <summary>
        /// insert entity
        /// </summary>
        /// <param name="entity">entity</param>
        public virtual async Task InsertAsync(T entity)
        {
            await Retry(async () =>
            {
                await Collection.InsertOneAsync(entity);
                return true;
            });
        }
        /// <summary>
        /// insert entity collection
        /// </summary>
        /// <param name="entities">collection of entities</param>
        public virtual void Insert(IEnumerable<T> entities)
        {

            Retry(() =>
            {
                Collection.InsertMany(entities);
                return true;
            });
        }
        /// <summary>
        /// insert entity collection
        /// </summary>
        /// <param name="entities">collection of entities</param>
        public virtual async Task InsertAsync(IEnumerable<T> entities)
        {
            await Retry(async () =>
            {
                await Collection.InsertManyAsync(entities);
                return true;
            });
        }
        #endregion Insert
    }
}
