using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSystem.Plane;
using AirTrafficMonitoringSystem.PlaneManager;
using AirTrafficMonitoringSystem.TransponderReceiverClient;
using Microsoft.SqlServer.Server;
using NSubstitute;
using NUnit.Framework;

namespace ATMS.Test.Unit
{
    
    [TestFixture]
    class PlaneManagerUnitTest
    {
        private List<Plane> NewPlanes = new List<Plane>();
        private List<Plane> OldPlanes = new List<Plane>();
        private PlaneManager uut;
        private ITransponderReceiverClient tr;

        [Test]
        public void QuickPlaneTest()
        {
            Plane p1 = new Plane
            {
                Altitude = 20000,
                ID = "TEST"
            };
            Plane p2 = new Plane
            {
                Altitude = 10000,
                ID = "TEST"
            };

            Assert.That(p1 == p2, Is.True);

            p2.ID = "EST";

            Assert.That(p1 != p2, Is.True);
        }

        [SetUp]
        public void Setup()
        {
            tr = Substitute.For<ITransponderReceiverClient>();
            uut = new PlaneManager(tr);
            uut.PlaneNotify += EventHandler;
        }

        public List<Plane> AddNewPlanes()
        {
            List<Plane> pl = new List<Plane>();
            pl.Add(new Plane
            {
                Altitude = 2000,
                ID = "FD200",
                TimeStamp = Parse("20191205121105200"),
                XPosition = 16000,
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

            return pl;
        }

        [Test]
        public void TestAddingNewPlanes()
        {

           List<Plane> pl = AddNewPlanes();
           tr.ItemArrivedReceived += Raise.Event<InformationReceivedHandler>
               (this, new PlaneDetectedEvent { planes = pl });

           
           Assert.That(NewPlanes, Is.EqualTo(pl));
        }

        [Test]
        public void TestCorrectID()
        {
            List<Plane> pl = AddNewPlanes();
            tr.ItemArrivedReceived += Raise.Event<InformationReceivedHandler>
                (this, new PlaneDetectedEvent {planes = pl});

            Assert.That(NewPlanes[0], Is.EqualTo(pl[0]));
        }

        [Test]
        public void NewPlanesNotUpdate()
        {
            List<Plane> pl = AddNewPlanes();
            tr.ItemArrivedReceived += Raise.Event<InformationReceivedHandler>
                (this, new PlaneDetectedEvent {planes = pl});

            Assert.That(OldPlanes != pl, Is.True);
        }
        
        [Test]
        public void TestUpdatePlanes()
        {
            List<Plane> pl = AddNewPlanes();
            tr.ItemArrivedReceived += Raise.Event<InformationReceivedHandler>
                (this, new PlaneDetectedEvent {planes = pl});

            List<Plane> pl2 = new List<Plane>();
            pl2.Add(new Plane
            {
                Altitude = 2000,
                ID = "FD200",
                TimeStamp = Parse("20191205121106723"),
                XPosition = 16000,
                YPosition = 18000
            });
            pl2.Add(new Plane
            {
                Altitude = 10000,
                ID = "FL100",
                TimeStamp = Parse("20191205121107300"),
                XPosition = 32000,
                YPosition = 11000
            });


            tr.ItemArrivedReceived += Raise.Event<InformationReceivedHandler>
                (this, new PlaneDetectedEvent {planes = pl2});

            Assert.That(OldPlanes[0], Is.EqualTo(pl2[0]));
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
