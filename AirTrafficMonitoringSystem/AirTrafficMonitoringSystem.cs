using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSystem.DataFormatter;
using AirTrafficMonitoringSystem.Display;
using AirTrafficMonitoringSystem.PlaneManager;
using AirTrafficMonitoringSystem.TransponderReceiverClient;
using TransponderReceiver;


namespace AirTrafficMonitoringSystem
{
    public class AirTrafficMonitoringSystem
    {
        private TransponderReceiverFactory TransponderReceiverFactory;
        private ITransponderReceiver TransponderReceiver;
        private IDataFormatter DataFormatter;
        private ITransponderReceiverClient TransponderReceiverClient;
        private IPlaneManager PlaneManager;
        private Display.Display Display;
        private ILogger Logger;

        public AirTrafficMonitoringSystem()
        {
            TransponderReceiverFactory = new TransponderReceiverFactory();
            TransponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            DataFormatter = new DataFormatter.DataFormatter();
            TransponderReceiverClient = new TransponderReceiverClient.TransponderReceiverClient(TransponderReceiver,DataFormatter);
            PlaneManager = new PlaneManager.PlaneManager(TransponderReceiverClient);
            Logger = new ConsoleLogger();
            Display = new Display.Display(PlaneManager,Logger);
        }
    }
}
