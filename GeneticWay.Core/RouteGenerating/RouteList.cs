using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.RouteGenerating
{
    public class RouteList
    {
        public List<Zone> Zones { get; }
        //public Zone? LastZone => Zones.LastOrDefault();

        public RouteList()
        {
            Zones = new List<Zone>();
        }

        private RouteList(IEnumerable<Zone> zones)
        {
            Zones = zones.Select(z => z).ToList();
        }

        public bool IsZoneAlreadyInList(Zone newZone)
        {
            return Zones.Any(z => z == newZone);
        }

        public void AddNew(Zone newZone)
        {
            if (IsZoneAlreadyInList(newZone))
                throw new ArgumentException("Already in list");

            Zones.Add(newZone);
        }

        public RouteList Clone()
        {
            return new RouteList(Zones);
        }
    }
}