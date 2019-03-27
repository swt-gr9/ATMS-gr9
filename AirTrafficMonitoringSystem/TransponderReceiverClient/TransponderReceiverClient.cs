using System;
using System.Collections.Generic;
using AirTrafficMonitoringSystem.DataFormatter;
using TransponderReceiver;

namespace AirTrafficMonitoringSystem.TransponderReceiverClient
{
   
    public class TransponderReceiverClient : ITransponderReceiverClient
    {
        private ITransponderReceiver receiver;
        private IDataFormatter _dataFormatter;
        public event InformationReceivedHandler ItemArrivedReceived;
        public TransponderReceiverClient(ITransponderReceiver receiver, IDataFormatter dataFormatter)
        {
            _dataFormatter = dataFormatter;
            this.receiver = receiver;

            
            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            List<Plane.Plane> tempPlanes = new List<Plane.Plane>();
            
            foreach (var data in e.TransponderData)
            {
                tempPlanes.Add(_dataFormatter.FormatFromString(data));
                //Console.WriteLine(data.ToString());
            }

            if (ItemArrivedReceived != null) ItemArrivedReceived(this, new PlaneDetectedEvent{planes = tempPlanes});
        }

        
    }
}
