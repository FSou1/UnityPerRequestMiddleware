using Microsoft.Practices.Unity;
using WebApiResource.Services;

namespace WebApiResource
{
    public class UnityConfig
    {
        public static IUnityContainer GetConfiguredContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            // ContainerControlledLifetimeManager - singleton's lifetime
            container.RegisterType<IAlwaysTheSame, AlwaysTheSame>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAlwaysTheSame, AlwaysTheSame>(new ContainerControlledLifetimeManager());

            // HierarchicalLifetimeManager - container's lifetime
            container.RegisterType<ISameInARequest, SameInARequest>(new HierarchicalLifetimeManager());

            // TransientLifetimeManager (RegisterType's default) - no lifetime
            container.RegisterType<IAlwaysDifferent, AlwaysDifferent>(new TransientLifetimeManager());
        }
    }
}