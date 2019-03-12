using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSystem.TransponderReceiverClient
{
    public delegate void InformationReceivedHandler(object o, List<Plane.Plane> planes);
    public interface ITransponderReceiverClient
    {
        event InformationReceivedHandler ItemArrivedReceived; 
    }
}
