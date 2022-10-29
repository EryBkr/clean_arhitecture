using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMemoryCache();
            serviceDescriptors.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceDescriptors.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            serviceDescriptors.AddSingleton<Stopwatch>();
        }
    }
}
