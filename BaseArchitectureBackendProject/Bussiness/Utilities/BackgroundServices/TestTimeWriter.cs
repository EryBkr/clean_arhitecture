using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Utilities.BackgroundServices
{
    public class TestTimeWriter : IHostedService, IDisposable
    {
        private Timer timer;

        public void Dispose() => timer = null;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(WriteTimerOnScreen, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            Console.WriteLine("Uygulama Çalıştı");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            Console.WriteLine("Uygulama durduruldu");
            return Task.CompletedTask;
        }

        private void WriteTimerOnScreen(object? state) => Console.WriteLine($"Date Time is {DateTime.Now.ToLongTimeString()}");

    }
}
