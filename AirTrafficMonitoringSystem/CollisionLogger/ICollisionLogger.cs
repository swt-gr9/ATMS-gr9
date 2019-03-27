using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSystem.PlaneManager;

namespace AirTrafficMonitoringSystem.CollisionLogger
{
    public interface ICollisionLogger
    {
        void LogPlanes(Planes p);
    }
}
