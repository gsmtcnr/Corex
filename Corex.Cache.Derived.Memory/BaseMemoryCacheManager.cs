using Corex.Cache.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Corex.Cache.Derived.Memory
{
    public abstract class BaseMemoryCacheManager : ICacheManager
    {

        public BaseMemoryCacheManager(string prefix = "")
        {
            Prefix = prefix;
        }
        protected static ObjectCache Cache => System.Runtime.Caching.MemoryCache.Default;
        public string Prefix { get; set; }
        public string GetKey(string key)
        {
            string format = "{0}-{1}";
            string returnValue = string.Format(format, Prefix, key);
            return returnValue;
        }

        public virtual bool IsSet(string key)
        {
            return (Cache.Contains(GetKey(key)));
        }
        public virtual bool Remove<T>(string key)
        {
            Cache.Remove(key);
            return true;
        }
        public virtual void Clear<T>()
        {
            foreach (var item in Cache)
                Remove<T>(item.Key);
        }

        public void Dispose()
        {
            MemoryCache memoryCache = (System.Runtime.Caching.MemoryCache)Cache;
            memoryCache.Dispose();
        }
        public T Get<T>(string key)
        {
            return (T)Cache[GetKey(key)];
        }
        public List<T> GetList<T>(string key)
        {
            return (List<T>)Cache[GetKey(key)];
        }
        public bool Set<T>(string key, T data, int cacheTime)
        {
            if (data == null)
                return false;

            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime)
            };
            return Cache.Add(new CacheItem(GetKey(key), data), cacheItemPolicy);
        }
    }
}
