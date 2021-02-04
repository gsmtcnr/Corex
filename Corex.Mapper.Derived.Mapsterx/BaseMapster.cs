using Corex.Mapper.Infrastructure;
using Mapster;

namespace Corex.Mapper.Derived.Mapsterx
{
    public abstract class BaseMapster : IMapping
    {
        public virtual TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TDestination>();
        }
    }
}
