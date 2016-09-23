using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Microsoft.Practices.Unity;

namespace WebApiResource
{
    public class ControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType
        )
        {
            // Get container that we set to OwinContext using common key
            var container = request.GetOwinContext().Get<IUnityContainer>("MyContainer");

            // Resolve requested IHttpController using current container
            // prevent DefaultControllerActivator's behaviour of creating child containers 
            var controller = (IHttpController)container.Resolve(controllerType);

            // Dispose container that would dispose each of container's registered service
            // Two ways of disposing container:
            // 1. At UnityContainerPerRequestMiddleware, after owin pipeline finished (WebAPI is just a part of pipeline)
            // 2. Here, after web api pipeline finished (if you do not use container at other middlewares) (uncomment next line)
            // request.RegisterForDispose(new Release(() => container.Dispose()));

            return controller;
        }
    }

    internal class Release : IDisposable
    {
        private readonly Action _release;

        public Release(Action release)
        {
            _release = release;
        }

        public void Dispose()
        {
            _release();
        }
    }
}