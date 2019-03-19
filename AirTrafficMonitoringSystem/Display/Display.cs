using AirTrafficMonitoringSystem.PlaneManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSystem.Display
{
    public class Display 
    {
        
        private IPlaneManager planeManager;

        public Display(IPlaneManager _planeManager)
        {
            planeManager = _planeManager;
            planeManager.PlaneNotify += DisplayPlaneInfo;
        }

        private void DisplayPlaneInfo(object sender, PlaneUpdateEvent e)
        {
            Console.Clear();
            
            foreach (var plane in e.NewPlanes)
            {
                printNewPlane(plane);

            }
            foreach(var plane in e.UpdatedPlanes)
            {
                printPlaneUpdate(plane);
            }

            foreach(var plane in e.CollidingPlanes)
            {
                Console.WriteLine($"{0} colliding with {1}", plane.New.ID, plane.Old.ID);
            }

        }

        private void printNewPlane(Plane.Plane plane)
        {
            int alt = plane.Altitude;
            string ID = plane.ID;
            int xPos = plane.XPosition;
            int yPos = plane.YPosition;
            Console.WriteLine("New Plane info:");
            Console.WriteLine("Plane ID: {0}", ID);
            Console.WriteLine("Plane Altitude: {0}", alt);
            Console.WriteLine("Plane Position, x: {0} , y:{1}", xPos, yPos);
        }

        private void printPlaneUpdate(Plane.Plane plane)
        {
            int alt = plane.Altitude;
            string ID = plane.ID;
            int xPos = plane.XPosition;
            int yPos = plane.YPosition;
            double heading = plane.Heading;
            double HorizontalSpeed = plane.HorizontalSpeed;
            Console.WriteLine("Updating Plane info");
            Console.WriteLine("Plane ID: {0}", ID);
            Console.WriteLine("Plane Altitude: {0}", alt);
            Console.WriteLine("Plane Position, x: {0} , y:{1}", xPos, yPos);
            Console.WriteLine("Plane Heading: {0}", heading);
            Console.WriteLine("Plane Horizontal speed: {0}", HorizontalSpeed);
        }
    }
}
