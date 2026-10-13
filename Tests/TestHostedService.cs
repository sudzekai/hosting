using Sudzekai.Hosting.Abstractions;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace Tests
{
    internal class TestHostedService : IHostedService, IAsyncDisposable
    {
        public TestHostedService()
        {
            Console.WriteLine($"hosted service ctor call executed");
        }

        public ValueTask DisposeAsync()
        {
            Console.WriteLine("async disposed");

            return ValueTask.CompletedTask;
        }

        public Task RunAsync()
        {
            Console.WriteLine("started");
            
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            Console.WriteLine("stopped");
            
            return Task.CompletedTask;
        }
    }
}
