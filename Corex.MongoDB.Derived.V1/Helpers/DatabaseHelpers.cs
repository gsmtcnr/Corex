using Corex.MongoDB.Derived.V1.Attributes;
using Corex.MongoDB.Inftrastructure;
using MongoDB.Driver;
using System;
using System.Reflection;

namespace Corex.MongoDB.Derived.V1.Helpers
{
    internal static class DatabaseHelpers<T> where T : class, IMongoModel
    {
        /// <summary>
        /// Creates and returns a MongoDatabase from the specified url.
        /// </summary>
        /// <param name="url">The url to use to get the database from.</param>
        /// <returns>Returns a MongoDatabase from the specified url.</returns>
        internal static IMongoDatabase GetDatabaseFromUrl(MongoUrl url)
        {
            var client = new MongoClient(url);
            return client.GetDatabase(url.DatabaseName); // WriteConcern defaulted to Acknowledged
        }

        /// <summary>
        /// Determines the connection name for T and assures it is not empty
        /// </summary>
        /// <typeparam name="T">The type to determine the connection name for.</typeparam>
        /// <returns>Returns the connection name for T.</returns>
        internal static string GetConnectionName()
        {
            var collectionName = typeof(T).GetTypeInfo().BaseType == typeof(object) ?
                GetConnectionNameFromInterface() :
                GetConnectionNameFromType();

            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(T).Name;
            }
            return collectionName.ToLowerInvariant();
        }

        /// <summary>
        /// Determines the connection name from the specified type.
        /// </summary>
        /// <typeparam name="T">The type to get the connection name from.</typeparam>
        /// <returns>Returns the connection name from the specified type.</returns>
        private static string GetConnectionNameFromInterface()
        {
            // Check to see if the object (inherited from Entity) has a ConnectionName attribute
            var att = typeof(T).GetTypeInfo().GetCustomAttribute(typeof(ConnectionNameAttribute));
            return (att != null) ? ((ConnectionNameAttribute)att).Name : typeof(T).Name;
        }

        /// <summary>
        /// Determines the connection name from the specified type.
        /// </summary>
        /// <param name="entitytype">The type of the entity to get the connection name from.</param>
        /// <returns>Returns the connection name from the specified type.</returns>
        private static string GetConnectionNameFromType()
        {
            Type entitytype = typeof(T);
            string collectionname;

            // Check to see if the object (inherited from Entity) has a ConnectionName attribute
            var att = entitytype.GetTypeInfo().GetCustomAttribute(typeof(ConnectionNameAttribute));
            if (att != null)
            {
                // It does! Return the value specified by the ConnectionName attribute
                collectionname = ((ConnectionNameAttribute)att).Name;
            }
            else
            {
                if (typeof(IMongoModel).GetTypeInfo().IsAssignableFrom(entitytype))
                {
                    // No attribute found, get the basetype
                    while (entitytype.GetTypeInfo().BaseType != typeof(IMongoModel))
                    {
                        entitytype = entitytype.GetTypeInfo().BaseType;
                    }
                }
                collectionname = entitytype.Name;
            }

            return collectionname;
        }


    }
}
