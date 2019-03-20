using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSystem.CollisionLogger;
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
        private List<Planes> CollidingPlanes = new List<Planes>();
        private PlaneManager uut;
        private ITransponderReceiverClient tr;
        private ICollisionLogger log;
        
        [SetUp]
        public void Setup()
        {
            tr = Substitute.For<ITransponderReceiverClient>();
            log = Substitute.For<ICollisionLogger>();
            uut = new PlaneManager(tr, log);
            uut.PlaneNotify += EventHandler;
        }

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

        
        [Test]
        public void TestPlaneRemoveOutOfAirSpace()
        {
            List<Plane> pl = AddNewPlanes();
            tr.ItemArrivedReceived += Raise.Event<InformationReceivedHandler>
                (this, new PlaneDetectedEvent {planes = pl});

            List<Plane> pl2 = new List<Plane>();
            pl2.Add(new Plane
            {
                Altitude = 2000,
                ID = "FD200",
                XPosition = 4000,
                YPosition = 2000
            });

            tr.ItemArrivedReceived += Raise.Event<InformationReceivedHandler>
                (this, new PlaneDetectedEvent {planes = pl2});

            Assert.That(!OldPlanes.Exists(p => p.ID == "FD200"), Is.True);

        }

        private void EventHandler(object sender, PlaneUpdateEvent e)
        {
            NewPlanes = e.NewPlanes;
            OldPlanes = e.UpdatedPlanes;
            CollidingPlanes = e.CollidingPlanes;
        }

        private DateTime Parse(string time)
        {
            return DateTime.ParseExact(time, "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
        }

        [TestCase(10000, 15000, 17000, 60000, false)]
        [TestCase(10000, 20000, 10000, 25000, false)]
        [TestCase(20000, 15000, 22000, 15000, true)]
        public void TestCollidingPlanes(int x1, int y1, int x2, int y2, bool result)
        {
            Plane p1 = new Plane {XPosition = x1, YPosition = y1, ID="E"};
            Plane p2 = new Plane {XPosition = x2, YPosition = y2, ID="F"};


            List<Plane> pl = new List<Plane>();
            pl.Add(p1);
            pl.Add(p2);

            tr.ItemArrivedReceived += Raise.Event<InformationReceivedHandler>
                (this, new PlaneDetectedEvent {planes = pl});


            Planes tmPlanes = new Planes {New = p1, Old = p2};

            Assert.That( CollidingPlanes.Count > 0, Is.EqualTo(result));

        }
        
    }


}
