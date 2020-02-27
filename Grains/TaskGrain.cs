using Component;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskGrainInterfaces;

namespace Grains
{
    public class TaskGrain : Orleans.Grain, ITaskGrain
    {
        private readonly ILogger logger;

        private static string TaskName = Guid.NewGuid().ToString();

        private MyComponent component;

        public TaskGrain(ILogger<TaskGrain> logger, MyComponent component)
        {
            this.logger = logger;
            this.component = component;
        }

        public override Task OnActivateAsync()
        {
            logger.LogInformation($"OnActivateAsync : TaskName = '{TaskName}'");
            return base.OnActivateAsync();
        }

        public override Task OnDeactivateAsync()
        {
            logger.LogInformation($"OnDeactivateAsync : TaskName = '{TaskName}'");

            return base.OnDeactivateAsync();
        }

        public Task<string> GetTaskName()
        {
            logger.LogInformation($"GetTaskName received.");
            component.Say();
            return Task.FromResult(TaskName);
        }

        public Task<string> GetTaskName(string taskId)
        {
            logger.LogInformation($"GetTaskName received: taskId = '{taskId}'");
            return Task.FromResult(TaskName);
        }

        public Task Notify(string taskId)
        {
            logger.LogInformation($"Notify: TaskId = '{taskId}', TaskName = '{TaskName}'");
            return Task.CompletedTask;
        }
    }
}
