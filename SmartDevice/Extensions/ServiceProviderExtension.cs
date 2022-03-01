using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Extensions
{
    public static class ServiceProviderExtension
    {
        public static IDisposable Scoped<T>(this IServiceProvider scopeFactory, out T service)
        {
            var scope = scopeFactory.CreateScope();
            var services = scope.ServiceProvider;
            service = services.GetRequiredService<T>();
            return scope;
        }
    }
}
