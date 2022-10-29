using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        //Kaç sn sonra performans zaafiyetinin oluştuğunu varsayabiliriz
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect(int interval = 5)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        //Metoda girmeden önce çalıştırıyorum öncelikte
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            //Performans zaafiyeti oluşmuşsa debug'a detaylarıyla yazıdıyoruz
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
                Debug.WriteLine($"Performans raporu: {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} ==> {_stopwatch.Elapsed.TotalSeconds}");

            //Stopwatch'ı sıfırladık
            _stopwatch.Reset();
        }
    }
}
