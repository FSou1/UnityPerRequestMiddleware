using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using WebApiResource.Services;

namespace WebApiResource.Middleware
{
    public class CustomMiddleware : OwinMiddleware
    {
        public CustomMiddleware(OwinMiddleware next) : base(next)
        {
            _next = next;
        }

        public override async Task Invoke(IOwinContext context)
        {
            // Get container that we set to OwinContext using common key
            var container = context.Get<IUnityContainer>(HttpApplicationKey.OwinPerRequestUnityContainerKey);

            // Resolve registered services
            var sameInARequest = container.Resolve<SameInARequest>();

            await _next.Invoke(context);
        }

        private readonly OwinMiddleware _next;
    }

    public static class CustomMiddlewareExtensions
    {
        public static IAppBuilder UseCustomMiddleware(this IAppBuilder app)
        {
            app.Use(typeof(CustomMiddleware));
            return app;
        }
    }
}