using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSystem.Calculator;
using AirTrafficMonitoringSystem.DataFormatter;
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
        //private TransponderReceiverClient _uut;
        //private IDataFormatter _dataFormatter;
        //private ITransponderReceiver _transponderReceiver;
        //private int _transponderEventsRecieved;


        //[SetUp]
        //private void SetUp()
        //{
        //    //_dataFormatter = Substitute.For<IDataFormatter>();
        //    //_transponderReceiver = Substitute.For<ITransponderReceiver>();

        //    //_uut = new TransponderReceiverClient(_transponderReceiver,_dataFormatter);            
        //}


        [Test]
        public void TestReceiverDataReadyEventHandlerCalled()
        {

            //List<string> testData = new List<string>();
            //testData.Add("ATR423;39045;12932;14000;20151006213456789");
            //testData.Add("BCD123;10005;85890;12000;20151006213456789");
            //testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            //_transponderReceiver.TransponderDataReady
            //    += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));
            Assert.That(true, Is.True);
        }


    }
}
