using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoringSystem.DataFormatter
{
    public class DataFormatter : IDataFormatter
    {

        public DataFormatter() { }

        public Plane.Plane FormatFromString(String planeinfo)
        {
            Plane.Plane planeicus = new Plane.Plane();
            String[] planeInfoSplit = planeinfo.Split(';');

            planeicus.ID = planeInfoSplit[0];
            planeicus.XPosition = Convert.ToInt32(planeInfoSplit[1]);
            planeicus.YPosition = Convert.ToInt32(planeInfoSplit[2]);
            planeicus.Altitude = Convert.ToInt32(planeInfoSplit[3]);
            string format = "yyyyMMddHHmmssfff";

            planeicus.TimeStamp = DateTime.ParseExact(planeInfoSplit[4], format, CultureInfo.InvariantCulture);

            //ATR423; 39045; 12932; 14000; 20151006213456789

            return planeicus;
        }
    }
}

