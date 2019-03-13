using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal.Execution;

namespace AirTrafficMonitoringSystem.TransponderReceiverClient
{
    public class PlaneDetectedEvent : EventArgs
    {
        public List<Plane.Plane> planes { get; set; }
    }

    public delegate void InformationReceivedHandler(object o, PlaneDetectedEvent e);
    public interface ITransponderReceiverClient
    {
        event InformationReceivedHandler ItemArrivedReceived; 
    }
}
