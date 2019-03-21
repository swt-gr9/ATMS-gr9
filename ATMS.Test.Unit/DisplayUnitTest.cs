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
        ILogger logger;

        [SetUp]
        public void Setup()
        {

            pm = Substitute.For<IPlaneManager>();
            logger = Substitute.For<ILogger>();

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

            logger.Received(1).LogText($"{p1.ID} colliding with {p2.ID}");
        }

        [TestCase("FJ20",14000, 39045, 12950)]
        public void TestNewPlaneDisplayID(string planeID, int alt, int xPos, int yPos)
        {
            List<Plane> testList = new List<Plane>();

            Plane p1 = new Plane { ID = planeID, Altitude = alt, XPosition = xPos, YPosition = yPos};

            testList.Add(p1);

            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent { NewPlanes = testList });

            logger.Received(1).LogText("New Plane info:");
            logger.Received(1).LogText("|      ID       |      Altitude      |      x-Position      |      y-Position      |");
            logger.Received(1).LogText("___________________________________________________________________________________________________");
            logger.Received(1).LogText($"|     {p1.ID}    |      {p1.Altitude}         |       {p1.XPosition}         |         {p1.YPosition}       |");
            logger.Received(1).LogText("---------------------------------------------------------------------------------------------------");

        }

        [TestCase("FJ20", 13567, 40000, 13000, 100.276, 300.276)]
        public void TestUpdatedPlaneDisplayID(string planeID,int alt, int xPos, int yPos, double _heading, double _hSpeed)
        {
            List<Plane> testList = new List<Plane>();

            Plane p1 = new Plane { ID = planeID, Altitude = alt, XPosition = xPos, YPosition = yPos, Heading = _heading, HorizontalSpeed = _hSpeed};

            testList.Add(p1);

            pm.PlaneNotify += Raise.Event<PlaneUpdate>(this, new PlaneUpdateEvent { UpdatedPlanes = testList });
            logger.Received(1).LogText("Updating Plane info:");
            logger.Received(1).LogText("|      ID       |      Altitude      |      x-Position      |      y-Position      |      Heading      |      Horizontal Speed      |");
            logger.Received(1).LogText($"|     {p1.ID}    |      {p1.Altitude}          |       {p1.XPosition}         |         {p1.YPosition}       |      {p1.Heading}    |      {p1.HorizontalSpeed}     |");
            logger.Received(1).LogText("-------------------------------------------------------------------------------------------------------------------------------------------");

        }
    }
}
