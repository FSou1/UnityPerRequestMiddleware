using System;
using System.Diagnostics;

namespace WebApiResource.Services
{
    public class AlwaysDifferent : IAlwaysDifferent, IDisposable
    {
        public void Dispose()
        {
            Trace.WriteLine("AlwaysDifferent: dispose");
        }
    }

    public interface IAlwaysDifferent
    {
    }
}