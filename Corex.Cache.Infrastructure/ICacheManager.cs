using Corex.Model.Infrastructure;
using System;
using System.Collections.Generic;

namespace Corex.Cache.Infrastructure
{
    public interface ICacheManager : IDisposable, ISingletonDependecy
    {
        string Prefix { get; set; }
        T Get<T>(string key);
        List<T> GetList<T>(string key);
        bool IsSet(string key);
        bool Set<T>(string key, T data, int cacheTime);
        bool Remove(string key);
        /// <param name="patternKey">ABC-*</param>
        bool RemovePattern(string patternKey);
        void Clear();
    }
}
