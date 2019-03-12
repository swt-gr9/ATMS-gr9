using System;

namespace AirTrafficMonitoringSystem.DataFormatter
{
    public interface IDataFormatter
    {
        Plane.Plane FormatFromString(String planeinfo);
    }
}