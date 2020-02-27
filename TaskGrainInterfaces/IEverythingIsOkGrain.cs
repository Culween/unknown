using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskGrainInterfaces
{
    public interface IEverythingIsOkGrain : IGrainWithGuidKey, IRemindable
    {
        Task Start();
        Task Stop();
    }
}
