﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyWebApp.Controllers
{
    public class ParallelAccessListController : Controller
    {
        private readonly ILogger<ParallelAccessListController> _logger;

        public ParallelAccessListController(ILogger<ParallelAccessListController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/parallel-list")]
        public async Task ParallelAsync()
        {
            var list = new List<int>();

            var tasks = new Task[10];

            //var context = new OneAtAtTimeSyncContext();
            //SynchronizationContext.SetSynchronizationContext(context);
            
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = GetNumberAsync(list, i);
            }

            await Task.WhenAll(tasks);

            SynchronizationContext.SetSynchronizationContext(null);
        }

        private async Task GetNumberAsync(List<int> results, int number)
        {
            await Task.Delay(300);
            _logger.LogInformation($"Thread Id : {Thread.CurrentThread.ManagedThreadId}");
            results.Add(number);
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
