using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskGrainInterfaces;

namespace Grains
{
    public class TaskGroupGrain : Orleans.Grain, ITaskGroupGrain
    {
        private readonly ILogger logger;

        private static Dictionary<Guid, string> Tasks = new Dictionary<Guid, string>();

        public TaskGroupGrain(ILogger<TaskGrain> logger)
        {
            this.logger = logger;
        }

        public async Task AddTask(Guid taskGrainKey)
        {
            ITaskGrain task = GrainFactory.GetGrain<ITaskGrain>(taskGrainKey);
            var taskName = await task.GetTaskName();

            Tasks.Add(taskGrainKey, taskName);

            LogTasks();

            return;
        }

        private void LogTasks()
        {
            foreach (var item in Tasks)
            {
                this.logger.LogInformation($"TaskGrainKey={item.Key}, TaskName={item.Value}");
            }
        }
    }
}
