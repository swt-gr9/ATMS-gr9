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

            double time = (second - first).TotalSeconds;
            Assert.That(Calculator.GetCurrentSpeed(deltaX, deltaY, time),
            Is.EqualTo(result).Within(0.001));
        }

        [TestCase(1,1,45.0)]
        [TestCase(1,0, 90.0)]
        [TestCase(-1, -1, 225)]
        public void TestHeading(int deltaX, int deltaY, double result)
        {
            Assert.That(Calculator.GetCurrentHeading(deltaX, deltaY), Is.EqualTo(result).Within(0.001));
        }

        [TestCase(0,0)]
        public void TestHeadingThrowsException(int deltaX, int deltaY)
        {
            Assert.That(() => Calculator.GetCurrentHeading(deltaX, deltaY), Throws.TypeOf<Calculator.PlaneNotMovingExeption>());
        }

       [TestCase(200, 500, 100, 400, true)]
       [TestCase(0, 0, 1000, 10000, false)]
        public void TestColliding(int xpos1, int ypos1, int xpos2, int ypos2, bool result)
        {
            Plane first = new Plane();
            first.XPosition = xpos1;
            first.YPosition = ypos1;

            Plane second = new Plane();
            second.XPosition = xpos2;
            second.YPosition = ypos2;

            Assert.That(Calculator.AreColliding(first.XPosition - second.XPosition, first.YPosition - second.YPosition),
                Is.EqualTo(result));
        }
    }
}
