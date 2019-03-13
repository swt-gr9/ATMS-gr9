using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSystem.Plane;
using AirTrafficMonitoringSystem.PlaneManager;
using AirTrafficMonitoringSystem.TransponderReceiverClient;
using NUnit.Framework;

namespace ATMS.Test.Unit
{
    
    [TestFixture]
    class PlaneManagerUnitTest
    {
        private List<Plane> NewPlanes = new List<Plane>();
        private List<Plane> OldPlanes = new List<Plane>();
        [Test]
        public void TestAddingNewPlanes()
        {
           List<Plane> pl = new List<Plane>();
           pl.Add(new Plane{Altitude = 2000,
               ID = "FD200",
               TimeStamp = Parse("20191205121105200"),
               XPosition = 5000,
               YPosition = 20000
           });
           pl.Add(new Plane
           {
               Altitude = 10000,
               ID = "FL100",
               TimeStamp = Parse("20191205121106300"),
               XPosition = 37000,
               YPosition = 10000
           });

           PlaneManager pm = new PlaneManager(new FakeTransponderRC(pl));
           pm.PlaneNotify += EventHandler;
           
           
           Assert.That(NewPlanes, Is.EqualTo(pl));
        }

        private void EventHandler(object sender, PlaneUpdateEvent e)
        {
            NewPlanes = e.NewPlanes;
            OldPlanes = e.UpdatedPlanes;
        }

        private DateTime Parse(string time)
        {
            return DateTime.ParseExact(time, "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
        }
    }
}
