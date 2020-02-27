using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskGrainInterfaces
{
    public interface ITaskGroupGrain : Orleans.IGrainWithGuidKey
    {
        Task AddTask(Guid taskGrainKey);
    }
}
