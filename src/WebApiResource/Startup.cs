using System.Web.Http;
using System.Web.Http.Dispatcher;
using Microsoft.Owin;
using Owin;
using Unity.WebApi;
using WebApiResource.Middleware;

[assembly: OwinStartup(typeof(WebApiResource.Startup))]
namespace WebApiResource
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure our parent container
            var container = UnityConfig.GetConfiguredContainer();
            
            // Pass our parent container to httpConfiguration
            var config = new HttpConfiguration {
                DependencyResolver = new UnityDependencyResolver(container)
            };

            // Use our own IHttpControllerActivator implementation 
            // (to prevent DefaultControllerActivator's behaviour of creating child containers per request)
            config.Services.Replace(typeof(IHttpControllerActivator), new ControllerActivator());

            WebApiConfig.Register(config);

            // Register our middleware, that should create ChildContainer and set it to OwinContext
            app.UseUnityContainerPerRequest(container);

            // Register other middlewares
            app.UseCustomMiddleware();
            app.UseWebApi(config);
        }
    }
}