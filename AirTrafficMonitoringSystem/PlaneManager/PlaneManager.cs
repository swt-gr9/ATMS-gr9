using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AirTrafficMonitoringSystem.TransponderReceiverClient;


namespace AirTrafficMonitoringSystem.PlaneManager
{

    public class PlaneManager : IPlaneManager
    {
        private List<Planes> CurrentPlanes;
        private PlaneUpdateEvent Event;
        private ITransponderReceiverClient Client;

        public PlaneManager(ITransponderReceiverClient _Client)
        {
            Client = _Client;
            Client.ItemArrivedReceived += AddPlane;
            CurrentPlanes = new List<Planes>();
        }
        private void AddPlane(object sender, PlaneDetectedEvent e)
        {
            Event = new PlaneUpdateEvent();
            foreach (var plane in e.planes)
            {
                if (CurrentPlanes.Contains(new Planes{New = plane}))
                {
                    UpdateExistingPlane(plane);
                    Event.UpdatedPlanes.Add(plane);
                }
                else
                {
                    AddNewPlane(plane);
                    Event.NewPlanes.Add(plane);
                }
            }
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
            int deltaY = p2.YPosition - p2.YPosition;
            double time = (p2.TimeStamp - p1.TimeStamp).TotalSeconds;


            CurrentPlanes[index].New.Heading = Calculator.Calculator.GetCurrentHeading(deltaX, deltaY);

            CurrentPlanes[index].New.HorizontalSpeed = Calculator.Calculator.GetCurrentSpeed(deltaX, deltaX, time);
            
        }
        
        public event PlaneUpdate PlaneNotify;
    }
}
