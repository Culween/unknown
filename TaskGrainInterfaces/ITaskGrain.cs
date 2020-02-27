using Orleans.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskGrainInterfaces
{
    public interface ITaskGrain : Orleans.IGrainWithGuidKey
    {
        Task<string> GetTaskName();

        Task<string> GetTaskName(string taskId);

        Task Notify(string taskId);
    }
}
