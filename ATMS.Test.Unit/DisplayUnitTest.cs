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

    }
}
