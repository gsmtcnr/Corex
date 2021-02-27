using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Corex.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Corex.Container.Derived.CastleWindsorContainer
{
    internal class DependecyInstaller : IWindsorInstaller
    {
        internal const string _mask = "*.*";
        internal static string _assemblyDirectoryName { get; } = string.Empty;
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            AssemblyFilter assemblyFilter = new AssemblyFilter(_assemblyDirectoryName, mask: _mask);
            RegisterSingleton(container, assemblyFilter);
            RegisterScoped(container, assemblyFilter);
            RegisterTransient(container, assemblyFilter);
            RegisterTransanction(container, assemblyFilter);
        }
        #region Private Methods
        private static void RegisterTransanction(IWindsorContainer container, AssemblyFilter assemblyFilter)
        {
            container.Register(Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<ITransactionDependecy>()
                     .WithServiceSelect(ServiceSelector).Configure(p => p.LifestyleScoped()));
        }

        private static void RegisterTransient(IWindsorContainer container, AssemblyFilter assemblyFilter)
        {
            container.Register(Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<ITransientDependecy>()
           .WithServiceSelect(ServiceSelector).LifestyleTransient());
        }

        private static void RegisterScoped(IWindsorContainer container, AssemblyFilter assemblyFilter)
        {
            container.Register(Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IScopedDependency>()
                     .WithServiceSelect(ServiceSelector).LifestyleScoped()
                     );
        }
        private static void RegisterSingleton(IWindsorContainer container, AssemblyFilter assemblyFilter)
        {
            container.Register(
                 Classes.FromAssemblyInDirectory(assemblyFilter)
                     .BasedOn<ISingletonDependecy>()
                     .WithServiceSelect(ServiceSelector).LifestyleSingleton()
                    );
        }

        private static IEnumerable<Type> ServiceSelector(Type type, Type[] baseTypes)
        {
            List<Type> result = type.GetInterfaces().ToList();
            result.Add(type);
            return result;
        }
        #endregion
    }
}
