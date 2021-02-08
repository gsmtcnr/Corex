using Corex.MongoDB.Inftrastructure;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Corex.MongoDB.Derived.V1.Repository
{
    public partial class Repository<T> where T : class, IMongoModel
    {
        /// <summary>
        /// delete entity
        /// </summary>
        /// <param name="entity">entity</param>
        public void Delete(T entity)
        {
            Delete(entity.Id);
        }
        /// <summary>
        /// delete entity
        /// </summary>
        /// <param name="entity">entity</param>
        public async Task DeleteAsync(T entity)
        {
            await DeleteAsync(entity.Id);
        }

        /// <summary>
        /// delete by id
        /// </summary>
        /// <param name="id">id</param>
        public virtual void Delete(Guid id)
        {
            Retry(() =>
            {
                return Collection.DeleteOne(i => i.Id == id);
            });
        }
        /// <summary>
        /// delete by id
        /// </summary>
        /// <param name="id">id</param>
        public virtual async Task DeleteAsync(Guid id)
        {
            await Retry(async () =>
            {
                return await Collection.DeleteOneAsync(i => i.Id == id);
            });
        }
        /// <summary>
        /// delete items with filter
        /// </summary>
        /// <param name="filter">expression filter</param>
        public void Delete(Expression<Func<T, bool>> filter)
        {
            Retry(() =>
            {
                return Collection.DeleteMany(filter);
            });
        }
        /// <summary>
        /// delete items with filter
        /// </summary>
        /// <param name="filter">expression filter</param>
        public async Task DeleteAsync(Expression<Func<T, bool>> filter)
        {
            await Retry(async () =>
            {
                return await Collection.DeleteManyAsync(filter);
            });
        }

    }
}
