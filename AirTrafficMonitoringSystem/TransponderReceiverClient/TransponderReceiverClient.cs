using System;
using System.Collections.Generic;
using AirTrafficMonitoringSystem.DataFormatter;
using TransponderReceiver;

namespace AirTrafficMonitoringSystem.TransponderReceiverClient
{
    public class TransponderReceiverClient
    {
        private ITransponderReceiver receiver;
        private IDataFormatter _dataFormatter;
        
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
            }
            
        }
    }
}
