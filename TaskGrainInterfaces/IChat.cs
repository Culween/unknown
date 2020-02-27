using Orleans;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskGrainInterfaces
{
    public interface IChat: IGrainObserver
    {
        /// <summary>
        /// Client receive message.
        /// </summary>
        /// <param name="message"></param>
        void ReceiveMessage(string message);
    }
}
