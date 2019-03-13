using System;
using AirTrafficMonitoringSystem.Plane;

namespace AirTrafficMonitoringSystem.Calculator
{
  
    public static class Calculator
    {
        /// <summary>
        /// Returnerer farten ud fra ændring i x og y koordinat og tiden imellem
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double GetCurrentSpeed(int deltaX, int deltaY, double timeInmSec)
        {
            return (double) GetDistance(deltaX, deltaY) / (timeInmSec/1000);
        }

        private static double GetDistance(int x, int y)
        {
            return Math.Sqrt((Math.Pow(x, 2) + Math.Pow(y, 2)));
        }

        /// <summary>
        /// Returnerer vinklen i forhold til y-aksen. 0 grader er stik nord
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        /// <returns></returns>
        public static double GetCurrentHeading(int deltaX, int deltaY)
        {
            if (deltaX == 0)
            {
                if (deltaY > 0)
                {
                    return 0.0;
                }
                else if (deltaY < 0)
                {
                    return 180.0;
                }
            }
            else if (deltaY == 0)
            {
                if (deltaX > 0)
                {
                    return 90.0;
                }
                else if (deltaX < 0)
                {
                    return 270.0;
                }
            }

            double Offset = CalculateOffset(deltaX, deltaY);


            return Offset + (Math.Atan(Math.Abs(deltaX / deltaY)) * 180/Math.PI);
        }

        public static bool AreColliding(int deltaX, int deltaY)
        {
            return (GetDistance(deltaX, deltaY) < 5000);
        }

        private static double CalculateOffset(int deltaX, int deltaY)
        {
            if (deltaX > 0)
            {
                if (deltaY > 0)
                {
                    return 0.0;
                }
                else
                {
                    return 90.0;
                }
            }
            else
            {
                if (deltaY > 0)
                {
                    return 270.0;
                }
                else
                {
                    return 180.0;
                }
            }
        }

        public static bool IsInsideAirSpace(int xpos, int ypos)
        {
            return (xpos >= 10000 && xpos <= 90000) && (ypos >= 10000 && ypos <= 90000);
        }
    }
}
