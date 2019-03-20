using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AirTrafficMonitoringSystem.CollisionLogger;
using AirTrafficMonitoringSystem.TransponderReceiverClient;


namespace AirTrafficMonitoringSystem.PlaneManager
{

    public class PlaneManager : IPlaneManager
    {
        private List<Planes> CurrentPlanes;
        private List<string> IDTracker;
        private PlaneUpdateEvent Event;
        private ITransponderReceiverClient Client;
        private ICollisionLogger ColLog;
        private List<int> CollideTracker;

        public PlaneManager(ITransponderReceiverClient _Client, ICollisionLogger log)
        {
            Client = _Client;
            ColLog = log;
            Client.ItemArrivedReceived += AddPlane;
            CurrentPlanes = new List<Planes>();
            IDTracker = new List<string>();
            CollideTracker = new List<int>();
        }
        private void AddPlane(object sender, PlaneDetectedEvent e)
        {
            Event = new PlaneUpdateEvent();
            foreach (var plane in e.planes)
            {

                if (Calculator.Calculator.IsInsideAirSpace(plane.XPosition, plane.YPosition))
                {
                    if (IDTracker.Contains(plane.ID))
                    {
                        UpdateExistingPlane(plane);
                        Event.UpdatedPlanes.Add(plane);
                    }
                    else
                    {
                        IDTracker.Add(plane.ID);
                        AddNewPlane(plane);
                        Event.NewPlanes.Add(plane);
                    }
                }
                else
                {
                    if (IDTracker.Contains(plane.ID))
                    {
                        IDTracker.Remove(plane.ID);
                        CurrentPlanes.Remove(CurrentPlanes.First(p => p.New.ID == plane.ID));
                    }
                }

            }
            
            TestCollidingPlanes();
            
            PlaneNotify?.Invoke(this, Event);

        }

        private void AddNewPlane(Plane.Plane Add)
        {
            Planes planonovic = new Planes();
            planonovic.New = Add;
            CurrentPlanes.Add(planonovic);
        }

        private void UpdateExistingPlane(Plane.Plane Update)
        {
            int index = CurrentPlanes.FindIndex(p => p.New == Update);
            
            CurrentPlanes[index].Old = CurrentPlanes[index].New;
            CurrentPlanes[index].New = Update;

            CalculateNewData(index);
        }

        private void CalculateNewData(int index)
        {
            Plane.Plane p1 = CurrentPlanes[index].Old;
            Plane.Plane p2 = CurrentPlanes[index].New;
            int deltaX = p2.XPosition - p1.XPosition;
            int deltaY = p2.YPosition - p1.YPosition;
            double time = (p2.TimeStamp - p1.TimeStamp).TotalMilliseconds;


            CurrentPlanes[index].New.Heading = Calculator.Calculator.GetCurrentHeading(deltaX, deltaY);

            CurrentPlanes[index].New.HorizontalSpeed = Calculator.Calculator.GetCurrentSpeed(deltaX, deltaX, time);
            
        }

        private void TestCollidingPlanes()
        {
            for (int i = 0; i < CurrentPlanes.Count; ++i)
            {
                for (int j = 0; j < CurrentPlanes.Count; ++j)
                {
                    if (i != j)
                    {
                        int deltaX = CurrentPlanes[i].New.XPosition - CurrentPlanes[j].New.XPosition;
                        int deltaY = CurrentPlanes[i].New.YPosition - CurrentPlanes[j].New.YPosition;
                        int deltaAlt = CurrentPlanes[i].New.Altitude - CurrentPlanes[j].New.Altitude;

                        if (Calculator.Calculator.AreColliding(deltaX, deltaY, deltaAlt))
                        {
                            Planes comPlanes = new Planes { New = CurrentPlanes[i].New, Old = CurrentPlanes[j].New };

                            if (!CollideTracker.Contains(comPlanes.GetHashCode()))
                            {
                                Event.CollidingPlanes.Add(comPlanes);
                                CollideTracker.Add(comPlanes.GetHashCode());
                                ColLog.LogPlanes(comPlanes);
                            }
                        }
                    }
                }
            }
        }
        
        public event PlaneUpdate PlaneNotify;
    }
}
