using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSystem.CollisionLogger;
using AirTrafficMonitoringSystem.Plane;
using AirTrafficMonitoringSystem.PlaneManager;
using NSubstitute.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATMS.Test.Unit
{
    [TestFixture]
    class CollisionLoggerUnitTest
    {
        private ICollisionLogger uut;
        private string Path;
        [SetUp]
        public void Setup()
        {
            uut = new CollisionLogger();
            Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        [Test]
        public void TestFileCreation()
        {
            Assert.That(File.Exists(Path + @"\LogFile.txt"), Is.True);
        }

        [TestCase("ID1", "ID2", "ID1ID2")]
        public void TestWritingToFile(string planeID1, string planeID2, string result)
        {
            Plane p1 = new Plane {ID = planeID1, TimeStamp = DateTime.Now};
            Plane p2 = new Plane {ID = planeID2};

            Planes combined = new Planes {New = p1, Old = p2};

            uut.LogPlanes(combined);
            string[] outPut = File.ReadAllLines(Path + @"\LogFile.txt");
            

            string[] IDs = outPut[0].Split(';');

            Assert.That(IDs[0] + IDs[1], Is.EqualTo(result));
        }
    }
}
