using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager
{
    public static class DelayMe
    {
        public static async Task DoAsync()
        {
            Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);
   
          
        }
    }
}
