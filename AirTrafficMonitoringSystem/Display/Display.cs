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
            logger.Clear();
            logger.LogText("New Plane info:");
            logger.LogText("|ID     |Altitude            |x-Position          |y-Position          |");
            logger.LogText("________________________________________________________________________");
            foreach (var plane in e.NewPlanes)
            {
                printNewPlane(plane);

            }
            logger.LogText("Updated Plane info:");
            logger.LogText("|ID     |Altitude            |x-Position          |y-Position          |Heading             |Horizontal Speed            |");
            logger.LogText("__________________________________________________________________________________________________________________________");
            foreach (var plane in e.UpdatedPlanes)
            {
                printPlaneUpdate(plane);
            }

            foreach(var plane in e.CollidingPlanes)
            {
                logger.LogText($"{plane.New.ID} colliding with {plane.Old.ID}");
            }
        }

        private void printNewPlane(Plane.Plane plane)
        {
            int alt = plane.Altitude;
            string _ID = plane.ID;
            int xPos = plane.XPosition;
            int yPos = plane.YPosition;
            
            logger.LogText($"|{_ID, -7}|{alt,-20}|{xPos,-20}|{yPos,-20}|");
            logger.LogText("------------------------------------------------------------------------");
        }

        private void printPlaneUpdate(Plane.Plane plane)
        {
            int alt = plane.Altitude;
            string _ID = plane.ID;
            int xPos = plane.XPosition;
            int yPos = plane.YPosition;
            double heading = plane.Heading;
            double HorizontalSpeed = plane.HorizontalSpeed;
            
            logger.LogText($"|{_ID, -7}|{alt, -20}|{xPos,-20}|{yPos,-20}|{heading,-20}|{HorizontalSpeed,-28}|");
            logger.LogText("--------------------------------------------------------------------------------------------------------------------------");

        }
    }
}
