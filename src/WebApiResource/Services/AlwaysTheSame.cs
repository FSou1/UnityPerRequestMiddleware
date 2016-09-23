using System;
using System.Diagnostics;

namespace WebApiResource.Services
{
    public class AlwaysTheSame : IAlwaysTheSame, IDisposable
    {
        public static AlwaysTheSame Create()
        {
            return new AlwaysTheSame();
        }

        public void Dispose()
        {
            Trace.WriteLine("AlwaysTheSame: dispose");
        }
    }

    public interface IAlwaysTheSame
    {
    }
}