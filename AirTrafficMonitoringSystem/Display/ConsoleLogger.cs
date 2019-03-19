using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSystem.Display
{
    public class ConsoleLogger : ILogger
    {
        public void LogText(string t)
        {
            Console.WriteLine(t);
        }
    }
}
