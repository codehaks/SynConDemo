using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyWebApp.Controllers
{
    public class ParallelTaskController : Controller
    {
        private readonly ILogger<ParallelTaskController> _logger;

        public ParallelTaskController(ILogger<ParallelTaskController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/parallel-task")]
        public async Task<IActionResult> ParallelAsync()
        {
            var list = new List<int>();

            var tasks = new Task<int>[10];

            //var context = new OneAtAtTimeSyncContext();
            //SynchronizationContext.SetSynchronizationContext(context);

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = GetNumberAsync(i);
            }

            var numbers=await Task.WhenAll(tasks);

            SynchronizationContext.SetSynchronizationContext(null);

            var result = 0;
            foreach (var number in numbers)
            {
                result += number;
            }
            return Ok(result);
        }

        private async Task<int> GetNumberAsync(int number)
        {
            await Task.Delay(300);
            _logger.LogInformation($"Thread Id : {Thread.CurrentThread.ManagedThreadId}");
            return number;
        }

        private class OneAtAtTimeSyncContext : SynchronizationContext
        {
            private Task _task = Task.CompletedTask;
            private object lockObj = new object();

            public override void Post(SendOrPostCallback d, object state)
            {
                lock (lockObj)
                {
                    _task = _task.ContinueWith(_ =>
                    {
                        d(state);
                    });
                }
            }
        }
    }
}