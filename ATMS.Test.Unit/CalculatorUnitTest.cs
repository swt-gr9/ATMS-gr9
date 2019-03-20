using NUnit.Framework;
using System;
using System.Globalization;
using AirTrafficMonitoringSystem.Plane;


namespace ATMS.Test.Unit
{
    
    using AirTrafficMonitoringSystem.Calculator;

    [TestFixture]
    class CalculatorTest
    {
        private string format = "yyyyMMddHHmmssfff";
        private string TimeOffset = "201912051105";

      
        [TestCase(5, 5, "02100", "02500", 17.6777)]
        [TestCase(10, 15, "02000", "02500", 36.0555)]
        [TestCase(-20, -10, "02000", "02100", 223.6068)]
        public void TestCurrentSpeed(int deltaX, int deltaY, string time1, string time2, double result)
        {
            
            DateTime first = DateTime.ParseExact(TimeOffset + time1, format, CultureInfo.InvariantCulture);
            DateTime second = DateTime.ParseExact(TimeOffset + time2, format, CultureInfo.InvariantCulture);

            double time = (second - first).TotalMilliseconds;
            Plane p = new Plane {HorizontalSpeed = Calculator.GetCurrentSpeed(deltaX, deltaY, time)};
            Assert.That(p.HorizontalSpeed, Is.EqualTo(result).Within(0.001));
        }

        [TestCase(1,1,45.0)]
        [TestCase(-1, -1, 225)]
        [TestCase(0, -1, 180.0)]
        [TestCase(1, -1, 135)]
        [TestCase(-1, 1, 315)]
        public void TestHeading(int deltaX, int deltaY, double result)
        {
            Plane calcPlane = new Plane {XPosition = deltaX, YPosition = deltaY};

            calcPlane.Heading = Calculator.GetCurrentHeading(calcPlane.XPosition, calcPlane.YPosition);

            Assert.That(calcPlane.Heading, Is.EqualTo(result).Within(0.001));
        }

       [TestCase(200, 500, 100, 400, 1000, 800, true)]
       [TestCase(0, 0, 1000, 10000, 20000, 15000, false)]
       [TestCase(0, 0, 0, 4000, 1000, 20000, false)]
       [TestCase(0, 0, 10000, 6000, 20000, 20000, false)]
        public void TestColliding(int xpos1, int ypos1, int xpos2, int ypos2, int alt1, int alt2, bool result)
        {
            Plane first = new Plane();
            first.XPosition = xpos1;
            first.YPosition = ypos1;
            first.Altitude = alt1;

            Plane second = new Plane();
            second.XPosition = xpos2;
            second.YPosition = ypos2;
            second.Altitude = alt2;

            int deltax = first.XPosition - second.XPosition;
            int deltay = first.YPosition - second.YPosition;
            int deltaAlt = first.Altitude - second.Altitude;


            Assert.That(Calculator.AreColliding(deltax, deltay, deltaAlt),
                Is.EqualTo(result));
        }

        [TestCase(5000, 20000, false)]
        [TestCase(13000, 12000, true)]
        [TestCase(10000, 90000, true)]
        public void TestIsInsideAirspace(int x, int y, bool result)
        {
            Plane p = new Plane {XPosition = x, YPosition = y};

            Assert.That(Calculator.IsInsideAirSpace(p.XPosition, p.YPosition), Is.EqualTo(result));
        }
    }
}
