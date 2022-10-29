using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Caching
{
    public class CacheAspect : MethodInterception
    {
        //Cache manager bizim cache için oluşturduğumuz servis
        private ICacheManager _cacheManager;
        private int _duration;

        public CacheAspect(int duration)
        {
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            _duration = duration;
        }

        //Olay esnasında dahil olunması için Intercept seçtik
        public override void Intercept(IInvocation invocation)
        {
            //User.GetList
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");

            //Parametreleri aldım
            var arguments = invocation.Arguments.ToList();

            //Gelen parametreye göre de key generate ettim
            var key = $"{methodName}({string.Join(",", arguments.Select(p => p?.ToString() ?? "<Null>"))})";

            //Eğer key'e ait data varsa onu dönüyorum (method çalışmadan önce)
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }

            //Method çalışıyor
            invocation.Proceed();

            //Dönecek olan datayı cache'e ekledik
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
