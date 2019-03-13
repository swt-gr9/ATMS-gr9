using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSystem.TransponderReceiverClient
{
    class FakeTransponderRC : ITransponderReceiverClient
    {
        public event InformationReceivedHandler ItemArrivedReceived;
    }
}
