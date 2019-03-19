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
        private ILogger logger;

        public Display(IPlaneManager _planeManager, ILogger _logger)
        {
            planeManager = _planeManager;
            planeManager.PlaneNotify += DisplayPlaneInfo;
            logger = _logger;
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
            string _ID = plane.ID;
            int xPos = plane.XPosition;
            int yPos = plane.YPosition;

            logger.LogText("New Plane info:");
            logger.LogText($"Plane ID: {_ID}");
            logger.LogText($"Plane Altitude: {alt}");
            logger.LogText($"Plane Postion, x: {xPos}, y: {yPos}");
        }

        private void printPlaneUpdate(Plane.Plane plane)
        {
            int alt = plane.Altitude;
            string _ID = plane.ID;
            int xPos = plane.XPosition;
            int yPos = plane.YPosition;
            double heading = plane.Heading;
            double HorizontalSpeed = plane.HorizontalSpeed;

            logger.LogText("Updating Plane info");
            logger.LogText($"Plane ID: {_ID}");
            logger.LogText($"Plane Altitude: {alt}");
            logger.LogText($"Plane Position, x: {xPos} , y:{yPos}");
            logger.LogText($"Plane Heading: {heading}");
            logger.LogText($"Plane Horizontal speed: {HorizontalSpeed}");
        }
    }
}
