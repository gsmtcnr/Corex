using Corex.Cache.Infrastructure;

namespace Corex.Operation.Inftrastructure
{
    public interface ICacheSettings
    {
        ICacheManager CacheManager { get; set; }
        string Prefix { get; set; }
        int CacheTime { get; set; }
    }
}
