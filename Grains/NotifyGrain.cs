using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskGrainInterfaces;

namespace Grains
{
    public class NotifyGrain : Grain, INotifyGrain
    {
        private ObserverSubscriptionManager<IChat> _subsManager;

        public override async Task OnActivateAsync()
        {
            _subsManager = new ObserverSubscriptionManager<IChat>();
            await base.OnActivateAsync();
        }

        public Task Subscribe(IChat observer)
        {
            if (!_subsManager.IsSubscribed(observer))
            {
                _subsManager.Subscribe(observer);
            }
            return Task.CompletedTask;
        }

        public Task UnSubscribe(IChat observer)
        {
            if (_subsManager.IsSubscribed(observer))
            {
                _subsManager.Unsubscribe(observer);
            }
            return Task.CompletedTask;
        }
    }
}
