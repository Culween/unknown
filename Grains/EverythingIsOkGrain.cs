using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskGrainInterfaces;

namespace Grains
{
    [StorageProvider(ProviderName = "store1")]
    public class EverythingIsOkGrain : Grain, IEverythingIsOkGrain
    {
        private readonly ILogger logger;

        IGrainReminder _reminder = null;

        public EverythingIsOkGrain(ILogger<TaskGrain> logger)
        {
            this.logger = logger;
        }
        public async Task ReceiveReminder(string reminderName, TickStatus status)
        {
            var taskGrain = GrainFactory.GetGrain<ITaskGrain>(Guid.Empty);

            logger.LogInformation($"Reminder={reminderName} tick.");
            logger.LogInformation($"Reminder: TaskGrainPrimaryKey={taskGrain.GetPrimaryKey().ToString()}, Name={await taskGrain.GetTaskName()}, it is Ok.");
        }

        public async Task Start()
        {
            if (_reminder != null)
            {
                return;
            }

            _reminder = await RegisterOrUpdateReminder(
                this.GetPrimaryKey().ToString(),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromMinutes(1));

            logger.LogInformation($"Reminder={this.GetPrimaryKeyString()} registered.");
        }

        public async Task Stop()
        {
            if (_reminder == null)
            {
                return;
            }

            await UnregisterReminder(_reminder);
            _reminder = null;
        }
    }
}
