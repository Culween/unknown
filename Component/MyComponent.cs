using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Component
{
    public class MyComponent : ILifecycleParticipant<IGrainLifecycle>
    {
        public static MyComponent Create(IGrainActivationContext context)
        {
            var component = new MyComponent();
            component.Participate(context.ObservableLifecycle);
            return component;
        }

        public void Participate(IGrainLifecycle lifecycle)
        {
            lifecycle.Subscribe<MyComponent>(GrainLifecycleStage.First, OnFirst);
            lifecycle.Subscribe<MyComponent>(GrainLifecycleStage.SetupState, OnSetupState);
            lifecycle.Subscribe<MyComponent>(GrainLifecycleStage.Activate, OnActivate);
            lifecycle.Subscribe<MyComponent>(GrainLifecycleStage.Last, OnLast);
        }

        private Task OnFirst(CancellationToken ct)
        {
            Console.WriteLine("MyComponent subscribe GrainLifecycleStage.First");
            return Task.CompletedTask;
        }

        private Task OnSetupState(CancellationToken ct)
        {
            Console.WriteLine("MyComponent subscribe GrainLifecycleStage.SetupState");
            return Task.CompletedTask;
        }

        private Task OnActivate(CancellationToken ct)
        {
            Console.WriteLine("MyComponent subscribe GrainLifecycleStage.Activate");
            return Task.CompletedTask;
        }

        private Task OnLast(CancellationToken ct)
        {
            Console.WriteLine("MyComponent subscribe GrainLifecycleStage.Last");
            return Task.CompletedTask;
        }

        public Task<string> Say()
        {
            return Task.FromResult("Hello");
        }
    }
}
