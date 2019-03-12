using NUnit.Framework;
using System;
using System.Globalization;



namespace ATMS.Test.Unit
{
    
    using AirTrafficMonitoringSystem.Calculator;

    [TestFixture]
    class CalculatorTest
    {
        private string format = "yyyyMMddHHmmssfff";
        private string TimeOffset = "201912051105";

        [TestCase(5, 5, "02100", "02500", 17.6777)]
        public void TestCurrentSpeed(int deltaX, int deltaY, string time1, string time2, double result)
        {
            DateTime first = DateTime.ParseExact(TimeOffset + time1, format, CultureInfo.InvariantCulture);
            DateTime second = DateTime.ParseExact(TimeOffset + time2, format, CultureInfo.InvariantCulture);

            double time = (second - first).TotalSeconds;
            Assert.That(Calculator.GetCurrentSpeed(deltaX, deltaY, time),
            Is.EqualTo(result).Within(4));
        }
    }
}
