using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSystem.Display
{
    public interface ILogger
    {
        void LogText(string t);
        void Clear();
    }
}
