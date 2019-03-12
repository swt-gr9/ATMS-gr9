using System;

namespace AirTrafficMonitoringSystem.Plane
{
    public class Plane
    {
        public string ID { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int Altitude { get; set; }
        public double HorizontalSpeed { get; set; }
        public double Heading { get; set; }
        public DateTime TimeStamp { get; set; }

        public static bool operator ==(Plane p1, Plane p2)
        {
            return p1.ID == p2.ID;
        }

        public static bool operator !=(Plane p1, Plane p2)
        {
            return p1.ID != p2.ID;
        }
    }
}
