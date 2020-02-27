using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskGrainInterfaces
{
    public interface INotifyGrain : IGrainWithGuidKey
    {
        Task Subscribe(IChat chart);

        Task UnSubscribe(IChat observer);
    }
}
