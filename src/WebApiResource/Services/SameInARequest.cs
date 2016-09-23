using System;
using System.Diagnostics;

namespace WebApiResource.Services
{
    public class SameInARequest : ISameInARequest, IDisposable
    {
        public void Dispose()
        {
            Trace.WriteLine("SameInARequest: dispose");
        }
    }

    public interface ISameInARequest
    {
    }
}