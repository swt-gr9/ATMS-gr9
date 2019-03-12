using NUnit.Framework;
using System;
using System.Globalization;
using AirTrafficMonitoringSystem.Calculator;
using AirTrafficMonitoringSystem.DataFormatter;
using AirTrafficMonitoringSystem.Plane;


namespace ATMS.Test.Unit
{



    [TestFixture]
    class DataFormatterTest
    {
        DataFormatter uut;

        [SetUp]
        public void setup()
        {
            uut = new DataFormatter();

        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789")]
        public void testFormatStringIDRecievedMatchesIDSent(string ThetestString)
        {

            Plane p = uut.FormatFromString(ThetestString);

            Assert.That(p.ID, Is.EqualTo("ATR423"));
           
        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789")]
        public void testFormatStringXpositionRecievedMatchesXpositionLogged(string ThetestString)
        {

            Plane p = uut.FormatFromString(ThetestString);

            Assert.That(p.XPosition, Is.EqualTo(39045));

        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789")]
        public void testFormatStringYRecievedMatchesYSent(string ThetestString)
        {

            Plane p = uut.FormatFromString(ThetestString);

            Assert.That(p.YPosition, Is.EqualTo(12932));

        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789")]
        public void testFormatStringAltitudeRecievedMatchesAltitudeSent(string ThetestString)
        {

            Plane p = uut.FormatFromString(ThetestString);

            Assert.That(p.Altitude, Is.EqualTo(14000));

        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789")]
        public void testFormatStringyearRecievedMatchesyearSent(string ThetestString)
        {

            Plane p = uut.FormatFromString(ThetestString);

            Assert.That(p.TimeStamp.Year, Is.EqualTo(2015));

        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789")]
        public void testFormatStringmillisecondsRecievedMatchesmilisecondsSent(string ThetestString)
        {

            Plane p = uut.FormatFromString(ThetestString);

            Assert.That(p.TimeStamp.Millisecond, Is.EqualTo(789));

        }
    }

}

