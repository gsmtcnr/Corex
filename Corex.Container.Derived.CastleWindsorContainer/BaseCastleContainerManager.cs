using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Corex.Container.Inftrastructure;
using Microsoft.Extensions.Configuration;
using System;


namespace Corex.Container.Derived.CastleWindsorContainer
{
    public abstract class BaseCastleContainerManager : IContainerManager
    {
        private WindsorContainer _container;
        public void Initialize(IConfigurationRoot configurationRoot)
        {
            _container = new WindsorContainer();
            _container.Register(Component.For(typeof(IConfigurationRoot)).Instance(configurationRoot).LifestyleSingleton());
            _container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(DependecyInstaller._assemblyDirectoryName, mask: DependecyInstaller._mask))
                  .BasedOn<IWindsorInstaller>()
                  .WithServiceBase()
                  .LifestyleTransient());
            foreach (var item in _container.ResolveAll<IWindsorInstaller>())
            {
                _container.Install(item);

            }
        }
        public T Resolve<T>()
        {
            using (BeginScope())
            {
                return _container.Resolve<T>();
            }
        }

        public T[] ResolveAll<T>()
        {
            using (BeginScope())
            {
                return _container.ResolveAll<T>();
            }
        }
        private IDisposable BeginScope()
        {
            return _container.BeginScope();
        }
    }
}
