using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;

namespace WebApiResource.Middleware
{
    public class UnityContainerPerRequestMiddleware : OwinMiddleware
    {
        public UnityContainerPerRequestMiddleware(OwinMiddleware next, IUnityContainer container) : base(next)
        {
            _next = next;
            _container = container;
        }

        public override async Task Invoke(IOwinContext context)
        {
            // Create child container (whose parent is global container)
            var childContainer = _container.CreateChildContainer();

            // Set created container to owinContext (to become available at other places using OwinContext.Get<IUnityContainer>(key))
            context.Set("MyContainer", childContainer);

            await _next.Invoke(context);

            // Dispose container that would dispose each of container's registered service
            childContainer.Dispose();
        }

        private readonly OwinMiddleware _next;
        private readonly IUnityContainer _container;
    }

    public static class UnityMiddlewareExtensions
    {
        public static IAppBuilder UseUnityContainerPerRequest(this IAppBuilder app, IUnityContainer container)
        {
            app.Use(typeof(UnityContainerPerRequestMiddleware), container);
            return app;
        }
    }
}