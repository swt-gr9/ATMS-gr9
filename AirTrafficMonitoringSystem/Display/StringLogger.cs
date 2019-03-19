using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSystem.Display
{
    public class StringLogger : ILogger
    {
        public List<string> TestArray { get; set; }

        public void LogText(string t)
        {
            TestArray.Add(t);
        }
    }
}
