using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSystem.Calculator;
using AirTrafficMonitoringSystem.DataFormatter;
using AirTrafficMonitoringSystem.Plane;
using AirTrafficMonitoringSystem.TransponderReceiverClient;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TransponderReceiver;

namespace ATMS.Test.Unit
{
    [TestFixture]
    class TransponderReceiverClientTest
    {
        private TransponderReceiverClient _uut;
        private IDataFormatter _dataFormatter;
        private ITransponderReceiver _transponderReceiver;
        private int _numberOfEvents;


        [SetUp]
        public void SetUp()
        {
            _numberOfEvents = 0;

            _dataFormatter = Substitute.For<IDataFormatter>();
            _transponderReceiver = Substitute.For<ITransponderReceiver>();

            _uut = new TransponderReceiverClient(_transponderReceiver, _dataFormatter);

            _uut.InformationReceived += (sender, args) => _numberOfEvents++;
        }

        

        [TestCase("ATR423;39045;12932;14000;20151006213456789")]
        [TestCase("BCD123;10005;85890;12000;20151006213456789")]
        [TestCase("XYZ987;25059;75654;4000;20151006213456789")]
        public void ReceiverOnTransponderDataReady_CreateEvent_DataformatterFormatStringCalled(string input)
        {
            List<string> testData = new List<string>();
            testData.Add(input);
            
            _transponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            _dataFormatter.Received(1).FormatFromString(input);
        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789")]
        [TestCase("BCD123;10005;85890;12000;20151006213456789")]
        [TestCase("XYZ987;25059;75654;4000;20151006213456789")]
        public void ReceiverOnTransponderDataReady_CreateEvent_InformationReceivedNumberOfEventsEqual1(string input)
        {
            
            List<string> testData = new List<string>();
            testData.Add(input);

            _transponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.AreEqual(1,_numberOfEvents);
        }
    }
}
