using Corex.Cache.Infrastructure;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Text.Json;

namespace Corex.Cache.Derived.Redis
{
    public abstract class BaseRedisCacheManager : ICacheManager
    {
        ConnectionMultiplexer Redis = null;
        public IDatabase RedisDB { get; protected set; }
        public IServer RedisServer { get; protected set; }
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
            RedisDB = Redis.GetDatabase();
            System.Net.EndPoint[] endpoints = Redis.GetEndPoints();
            RedisServer = Redis.GetServer(endpoints[0]);
        }
        private string GetKey(string key)
        {
            string format = "{0}-{1}";
            string returnValue = string.Format(format, Prefix, key);
            return returnValue;
        }
        public virtual string Prefix { get; set; }
        public void Dispose()
        {
            Redis.Dispose();
            Redis.Close();
        }
        public bool Set<T>(string key, T data, int cacheTime)
        {
            string serializedValue = JsonSerializer.Serialize<T>(data);
            return RedisDB.StringSet(GetKey(key), serializedValue);
        }
        public bool IsSet(string key)
        {
            return RedisDB.KeyExists(GetKey(key));
        }
        public bool Remove(string key)
        {
            return RedisDB.KeyDelete(GetKey(key));
            //T value = Get<T>(key);
            //var serializedValue = JsonSerializer.Serialize<T>(value);
            //var response = db.SetRemove(GetKey(key), serializedValue);
        }
        public T Get<T>(string key)
        {
            var value = RedisDB.StringGet(GetKey(key));
            if (value.IsNullOrEmpty)
                return default(T);
            T resultModel = JsonSerializer.Deserialize<T>(value);
            return resultModel;
        }

        public List<T> GetList<T>(string key)
        {
            var valueList = RedisDB.ListRange(GetKey(key));
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

        public bool RemovePattern(string patternKey)
        {
            IEnumerable<RedisKey> keys = RedisServer.Keys(pattern: $"{patternKey}*", pageSize: 5000); // I am not sure the use of pageSize here.
            foreach (var item in keys)
            {
                Remove(item);
            }
            return true;
        }
        public void Clear()
        {
            var endpoints = Redis.GetEndPoints();
            var server = Redis.GetServer(endpoints[0]);
            server.FlushAllDatabases();
        }
    }
}
