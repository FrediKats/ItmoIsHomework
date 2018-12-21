using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticWay.Core.Models
{
    public class RouteList
    {
        private List<Zone> _zones;

        public RouteList()
        {
            _zones = new List<Zone>();
        }

        public bool IsInList(Zone newZone)
        {
            return _zones.Any(z => z == newZone);
        }

        public void AddNew(Zone newZone)
        {
            if (IsInList(newZone))
                throw new ArgumentException("Already in list");

            _zones.Add(newZone);
        }
    }
}