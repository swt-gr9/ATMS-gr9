using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoringSystem.PlaneManager;

namespace AirTrafficMonitoringSystem.CollisionLogger
{

    public class CollisionLogger : ICollisionLogger
    {
        private List<Planes> LoggedPlanes;
        private string Path;

        public CollisionLogger()
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\LogFile.txt";
            if (!File.Exists(Path))
            {
                var stream = File.Create(Path);
                stream.Close();
            }
            LoggedPlanes = new List<Planes>();
        }
        
        public void LogPlanes(Planes p)
        {
            if (!IsLogged(p))
            {
                File.AppendAllText(Path,$"{p.New.ID};{p.Old.ID};{p.New.TimeStamp.ToString()}" + Environment.NewLine);
                LoggedPlanes.Add(p);
            }
        }

        private bool IsLogged(Planes p)
        {
            return LoggedPlanes.Exists(pl => (pl.New == p.New && pl.Old == p.Old) || (pl.New == pl.Old && pl.Old == pl.New));
        }
    }
}
