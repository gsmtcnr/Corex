using Corex.Model.Infrastructure;

namespace Corex.Mapper.Infrastructure
{
    public interface IMapping : ISingletonDependecy
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
