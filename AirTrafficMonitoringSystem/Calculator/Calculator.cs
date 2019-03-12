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
        public static double GetCurrentSpeed(int deltaX, int deltaY, double time)
        {
            return (double)GetDistance(deltaX, deltaY) / time;
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
                else
                {
                    throw new PlaneNotMovingExeption();
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
                else
                {
                    throw  new PlaneNotMovingExeption();
                }
            }

            double Offset = 0;

            

            return Math.Atan(deltaX / deltaY);
        }

        public static bool AreColliding(int deltaX, int deltaY)
        {
            return (GetDistance(deltaX, deltaY) > 5000);
        }

        private double CalculateOffset(int deltaX, deltaY)
        {
            switch (deltaX)
            {
                
            }
        }
    }

    public class PlaneNotMovingExeption : System.Exception
    {
        public PlaneNotMovingExeption()
        {
           
        }
    }
}
