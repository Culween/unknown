using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskGrainInterfaces
{
    public interface ISlowpokeGrain : IGrainWithIntegerKey
    {
        Task GoSlow();

        [AlwaysInterleave]
        Task GoFast();
    }
}
