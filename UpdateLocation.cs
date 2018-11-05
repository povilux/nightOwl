using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace nightOwl
{
    class UpdateLocation
    {
        private GeoCoordinateWatcher Watcher = null;
        double CordX;
        double CordY;

        public void Update(Person person)
        {
            Location();
            person.CoordX = CordX;
            person.CoordY = CordY;
        }

        public void Location()
        {
            Watcher = new GeoCoordinateWatcher();
            Watcher.StatusChanged += Watcher_StatusChanged;
            Watcher.Start();
        }

        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Ready)
            {
                   CordX = Watcher.Position.Location.Latitude;
                   CordY = Watcher.Position.Location.Longitude;
            }
        }


    }
}
