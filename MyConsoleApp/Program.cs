using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            System.Diagnostics.Trace.WriteLine($"Before delay ...{Thread.CurrentThread.ManagedThreadId}");
            await DoAsync();
            System.Diagnostics.Trace.WriteLine($"After delay ...{Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("Done!");

        }

        private static async Task DoAsync()
        {
            

            System.Diagnostics.Trace.WriteLine($"DoAsync start ...{Thread.CurrentThread.ManagedThreadId}");
            var clinet = new HttpClient();
            //await clinet.GetAsync("https://codehaks.com");
            await Task.Delay(2000);
            await Task.Run(() =>
            {
                System.Diagnostics.Trace.WriteLine($"DoAsync run ...{Thread.CurrentThread.ManagedThreadId}");
            });
            System.Diagnostics.Trace.WriteLine($"DoAsync end ...{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
