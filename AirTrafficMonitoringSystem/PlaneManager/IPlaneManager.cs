﻿using System;
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
    }

    public delegate void PlaneUpdate(object sender, PlaneUpdateEvent e);
    public class PlaneUpdateEvent
    {
        public List<Plane.Plane> NewPlanes { get; set; } = new List<Plane.Plane>();
        public List<Plane.Plane> UpdatedPlanes { get; set; } = new List<Plane.Plane>();
    }
    interface IPlaneManager
    {
        void AddPlane(object sender, List<Plane.Plane> e);
        event PlaneUpdate PlaneNotify;
    }
}