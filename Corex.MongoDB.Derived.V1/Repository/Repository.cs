using Corex.MongoDB.Derived.V1.Helpers;
using Corex.MongoDB.Inftrastructure;
using MongoDB.Driver;
using Polly;
using Polly.Retry;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Corex.MongoDB.Derived.V1.Repository
{
    /// <summary>
    /// repository implementation for mongo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Repository<T> : IRepository<T>
        where T : class, IMongoModel
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="connectionString">connection string</param>
        public Repository(string connectionString)
        { 
            Collection = CollectionHelpers<T>.GetCollectionFromConnectionString(connectionString);
        }

        /// <summary>
        /// mongo collection
        /// </summary>
        public IMongoCollection<T> Collection
        {
            get; private set;
        }

        /// <summary>
        /// filter for collection
        /// </summary>
        public FilterDefinitionBuilder<T> Filter => Builders<T>.Filter;

        /// <summary>
        /// projector for collection
        /// </summary>
        public ProjectionDefinitionBuilder<T> Project => Builders<T>.Projection;


        private IFindFluent<T, T> Query(Expression<Func<T, bool>> filter)
        {
            return Collection.Find(filter);
        }

        private IFindFluent<T, T> Query()
        {
            return Collection.Find(Filter.Empty);
        }
        #region Simplicity

        /// <summary>
        /// validate if filter result exists
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>true if exists, otherwise false</returns>
        public bool Any(Expression<Func<T, bool>> filter)
        {
            return Retry(() => Collection.AsQueryable<T>().Any(filter));
        }

        #endregion Simplicity

        #region RetryPolicy
        /// <summary>
        /// retry operation for three times if IOException occurs
        /// </summary>
        /// <typeparam name="TResult">return type</typeparam>
        /// <param name="action">action</param>
        /// <returns>action result</returns>
        /// <example>
        /// return Retry(() => 
        /// { 
        ///     do_something;
        ///     return something;
        /// });
        /// </example>
        protected virtual TResult Retry<TResult>(Func<TResult> action)
        {
            return RetryPolicy
                .Handle<MongoConnectionException>(i => i.InnerException.GetType() == typeof(IOException))
                .Retry(3)
                .Execute(action);
        }
        #endregion
    }
}
