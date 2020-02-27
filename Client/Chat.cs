using System;
using System.Collections.Generic;
using System.Text;
using TaskGrainInterfaces;

namespace Client
{
    public class Chat : IChat
    {
        public void ReceiveMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
