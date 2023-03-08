using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.WebAPIClientLibrary.Models
{
    internal class ManagerConfiguration
    {
        public int DelayOnTooManyRequests { get; private set; }
        public int DelayBetweenRequests { get; private set; }

        public ManagerConfiguration(int delayOnTooManyRequest, int delayBetweenRequests)
        {
            DelayOnTooManyRequests = delayOnTooManyRequest;
            DelayBetweenRequests = delayBetweenRequests;
        }
    }
}
