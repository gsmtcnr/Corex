using Corex.Model.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Corex.Container.Inftrastructure
{
    public interface IContainerManager : ISingletonDependecy
    {
        void Initialize(IConfigurationRoot configurationRoot);
        T Resolve<T>();
        T[] ResolveAll<T>();
    }
}
