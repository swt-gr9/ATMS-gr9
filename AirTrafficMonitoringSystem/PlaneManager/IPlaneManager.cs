    using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSystem.PlaneManager
{
    public class Planes
    {
        public Plane.Plane New { get; set; }
        public Plane.Plane Old { get; set; }

        public static bool operator ==(Planes p1, Planes p2)
        {
            return p1.New == p2.New;
        }

        public static bool operator !=(Planes p1, Planes p2)
        {
            return p1.New != p2.New;
        }
    }

    public delegate void PlaneUpdate(object sender, PlaneUpdateEvent e);     
    public class PlaneUpdateEvent
    {
        public List<Plane.Plane> NewPlanes { get; set; } = new List<Plane.Plane>();
        public List<Plane.Plane> UpdatedPlanes { get; set; } = new List<Plane.Plane>();
    }
    public interface IPlaneManager
    {
        event PlaneUpdate PlaneNotify;
    }
}
