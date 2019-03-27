using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSystem.CollisionLogger;
using AirTrafficMonitoringSystem.DataFormatter;
using AirTrafficMonitoringSystem.Display;
using AirTrafficMonitoringSystem.PlaneManager;
using AirTrafficMonitoringSystem.TransponderReceiverClient;
using TransponderReceiver;

namespace ATMS.Applikation
{
    class Program
    {
        static void Main(string[] args)
        {
            TransponderReceiverFactory TransponderReceiverFactory = new TransponderReceiverFactory();
            ITransponderReceiver TransponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            IDataFormatter DataFormatter = new DataFormatter();
            ITransponderReceiverClient TransponderReceiverClient = new TransponderReceiverClient(TransponderReceiver, DataFormatter);
            ICollisionLogger ColLog = new CollisionLogger();
            IPlaneManager PlaneManager = new PlaneManager(TransponderReceiverClient, ColLog);
            ILogger Logger = new ConsoleLogger();
            Display Display = new Display(PlaneManager, Logger);
            
            while (true)
            {

            }
        }
    }
}
