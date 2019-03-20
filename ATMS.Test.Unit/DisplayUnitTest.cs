using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AirTrafficMonitoringSystem.Display;
using AirTrafficMonitoringSystem.PlaneManager;
using NSubstitute;
using AirTrafficMonitoringSystem;
using AirTrafficMonitoringSystem.Plane;

namespace ATMS.Test.Unit
{
    [TestFixture]
    class DisplayUnitTest
    {
        Display uut;
        IPlaneManager pm;
        StringLogger logger;

        [SetUp]
        public void Setup()
        {

            pm = NSubstitute.Substitute.For<IPlaneManager>();
            logger = new StringLogger();

            uut = new Display(pm, logger);
        }

        [TestCase("FR19", "TM17")]
        public void TestCollidingDisplay(string plane1, string plane2) 
        {
            List<Planes> testList = new List<Planes>();

            Plane p1 = new Plane { ID = plane1};
            Plane p2 = new Plane { ID = plane2 };

            Planes testPlanes = new Planes { New = p1, Old = p2 };

            testList.Add(testPlanes);

            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent {CollidingPlanes = testList });


            Assert.That(logger.TestArray[0], Is.EqualTo($"{p1} colliding with {p2}"));
        }

        [TestCase("FJ20")]
        //[TestCase("JK60", 15000, 40000, 13000)]
        public void TestNewPlaneDisplayID(string planeID)
        {
            List<Plane> testList = new List<Plane>();

            Plane p1 = new Plane{ID = planeID};
            
            testList.Add(p1);

            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent {NewPlanes = testList});
            Assert.That(logger.TestArray[0], Is.EqualTo("New Plane info:"));
            Assert.That(logger.TestArray[1], Is.EqualTo($"Plane ID: {p1.ID}"));
        }

        [TestCase(14000)]
        public void TestNewPlaneDisplayAltitude(int alt)
        {
            List<Plane> testList = new List<Plane>();

            Plane p1 = new Plane{Altitude = alt};

            testList.Add(p1);
            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent { NewPlanes = testList });
            Assert.That(logger.TestArray[2], Is.EqualTo($"Plane Altitude: {p1.Altitude}"));
        }

        [TestCase(39045, 12950)]
        public void TestNewPlaneDisplayPosition(int xPos, int yPos)
        {
            List<Plane> testList = new List<Plane>();

            Plane p1 = new Plane { XPosition = xPos, YPosition = yPos};

            testList.Add(p1);
            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent { NewPlanes = testList });
            Assert.That(logger.TestArray[0], Is.EqualTo($"Plane Postion, x: {p1.XPosition}, y: {p1.YPosition}"));
        }

        [TestCase("FJ20")]
        //[TestCase("JK60", 15000, 40000, 13000)]
        public void TestUpdatedPlaneDisplayID(string planeID)
        {
            List<Plane> testList = new List<Plane>();

            Plane p1 = new Plane { ID = planeID };

            testList.Add(p1);

            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent { UpdatedPlanes = testList });
            //Assert.That(logger.TestArray[0], Is.EqualTo("New Plane info:"));
            Assert.That(logger.TestArray[0], Is.EqualTo($"Plane ID: {p1.ID}"));
        }

        [TestCase(13567)]
        public void TestUpdatedPlaneDisplayAltitude(int alt)
        {
            List<Plane> testList = new List<Plane>();

            Plane p1 = new Plane { Altitude = alt };

            testList.Add(p1);
            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent { UpdatedPlanes = testList});
            Assert.That(logger.TestArray[0], Is.EqualTo($"Plane Altitude: {p1.Altitude}"));
        }

        [TestCase(40000, 13000)]
        public void TestUpdatedPlaneDisplayPos(int xPos, int yPos)
        {
            List<Plane> testList = new List<Plane>();

            Plane p1 = new Plane { XPosition = xPos, YPosition = yPos};

            testList.Add(p1);
            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent { UpdatedPlanes = testList });
            Assert.That(logger.TestArray[0], Is.EqualTo($"Plane Position, x: {p1.XPosition} , y:{p1.YPosition}"));
        }

        [TestCase(300.276)]
        public void TestUpdatedPlaneDisplayHeading(double _heading)
        {
            List<Plane> testList = new List<Plane>();

            Plane p1 = new Plane { Heading = _heading};

            testList.Add(p1);
            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent { UpdatedPlanes = testList });
            Assert.That(logger.TestArray[0], Is.EqualTo($"Plane Heading: {p1.Heading}"));
        }

        [TestCase(300.276)]
        public void TestUpdatedPlaneDisplayHorizontalSpeed(double _hSpeed)
        {
            List<Plane> testList = new List<Plane>();

            Plane p1 = new Plane { HorizontalSpeed = _hSpeed};

            testList.Add(p1);
            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent { UpdatedPlanes = testList });
            Assert.That(logger.TestArray[0], Is.EqualTo($"Plane Heading: {p1.HorizontalSpeed}"));
        }





    }
}
