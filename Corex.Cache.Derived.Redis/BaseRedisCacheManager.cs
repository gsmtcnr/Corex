using Corex.Cache.Infrastructure;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Text.Json;

namespace Corex.Cache.Derived.Redis
{
    public abstract class BaseRedisCacheManager : ICacheManager
    {
        ConnectionMultiplexer Redis = null;
        public abstract string GetConnectionString();
        private readonly string _connectionString;
        public BaseRedisCacheManager()
        {
            _connectionString = GetConnectionString();
            Connect();
        }
        private void Connect()
        {
            if ((Redis?.IsConnected).GetValueOrDefault())
            {
                return;

            }
            Redis = ConnectionMultiplexer.Connect(_connectionString);
        }
        private string GetKey(string key)
        {
            string format = "{0}-{1}";
            string returnValue = string.Format(format, Prefix, key);
            return returnValue;
        }
        public string Prefix { get; set; }
        public void Dispose()
        {
            Redis.Dispose();
            Redis.Close();
        }
        public bool Set<T>(string key, T data, int cacheTime)
        {
            IDatabase db = Redis.GetDatabase();
            string serializedValue = JsonSerializer.Serialize<T>(data);
            return db.StringSet(GetKey(key), serializedValue);
        }
        public bool IsSet(string key)
        {
            IDatabase db = Redis.GetDatabase();
            return db.KeyExists(key);

        }
        public bool Remove<T>(string key)
        {
            var db = Redis.GetDatabase();
            T value = Get<T>(key);
            var serializedValue = JsonSerializer.Serialize<T>(value);
            var response = db.SetRemove(GetKey(key), serializedValue);
            return response;
        }
        public T Get<T>(string key)
        {
            var db = Redis.GetDatabase();
            var value = db.StringGet(GetKey(key));
            if (value.IsNullOrEmpty)
                return default(T);

            T resultModel = JsonSerializer.Deserialize<T>(value);
            return resultModel;
        }

        public List<T> GetList<T>(string key)
        {
            var db = Redis.GetDatabase();
            var valueList = db.ListRange(GetKey(key));

            List<T> resultList = new List<T>();

            foreach (var item in valueList)
            {
                if (item.IsNullOrEmpty)
                {
                    continue;
                }
                
                T value = JsonSerializer.Deserialize<T>(item);
                resultList.Add(value);
            }
            return resultList;
        }
    }
}
