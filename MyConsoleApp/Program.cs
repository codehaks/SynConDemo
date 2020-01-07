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

            System.Diagnostics.Trace.WriteLine($"Before DoAsync...{Thread.CurrentThread.ManagedThreadId}");   

            await DoAsync();//.ConfigureAwait(true);
            System.Diagnostics.Trace.WriteLine($"After DoAsync ...{Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("Done!");
            System.Diagnostics.Trace.WriteLine($" Done ...{Thread.CurrentThread.ManagedThreadId}");
        }

        private static async Task DoAsync()
        {


            System.Diagnostics.Trace.WriteLine($"DoAsync start ...{Thread.CurrentThread.ManagedThreadId}");

            await DownLoad();

            System.Diagnostics.Trace.WriteLine($"DoAsync downloaded ...{Thread.CurrentThread.ManagedThreadId}");
            var t1 = Thread.CurrentThread.ManagedThreadId;
            await Task.Run(() =>
            {
                System.Diagnostics.Trace.WriteLine($"DoAsync run ...{Thread.CurrentThread.ManagedThreadId}");
                var t2 = Thread.CurrentThread.ManagedThreadId;

                System.Diagnostics.Trace.WriteLine($"Jumped from {t1} to {t2}");
            });
            
            System.Diagnostics.Trace.WriteLine($"DoAsync end ...{Thread.CurrentThread.ManagedThreadId}");
        }

        private static async Task DownLoad()
        {
            var t1 = Thread.CurrentThread.ManagedThreadId;
            var clinet = new HttpClient();
            await clinet.GetAsync("https://codehaks.com");
            //await Task.Delay(1000);
            var t2 = Thread.CurrentThread.ManagedThreadId;
            

            System.Diagnostics.Trace.WriteLine($"Jumped from {t1} to {t2}");
        }
    }

   
}
