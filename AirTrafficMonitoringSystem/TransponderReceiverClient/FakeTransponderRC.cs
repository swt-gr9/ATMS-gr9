using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSystem.TransponderReceiverClient
{
    public class FakeTransponderRC : ITransponderReceiverClient
    {
        public event InformationReceivedHandler ItemArrivedReceived;
        private List<Plane.Plane> FakeList;

        public FakeTransponderRC(List<Plane.Plane> GiveList)
        {
            FakeList = GiveList;
            Run();
        }

        private void Run()
        {
            ItemArrivedReceived?.Invoke(this, FakeList);
        }
    }
}
