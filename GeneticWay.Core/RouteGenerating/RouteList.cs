using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.RouteGenerating
{
    public class RouteList
    {
        private readonly List<Zone> _zones;
        public Zone? LastZone => _zones.LastOrDefault();

        public RouteList()
        {
            _zones = new List<Zone>();
        }

        private RouteList(IEnumerable<Zone> zones)
        {
            _zones = zones.Select(z => z).ToList();
        }

        public bool IsZoneAlreadyInList(Zone newZone)
        {
            return _zones.Any(z => z == newZone);
        }

        public void AddNew(Zone newZone)
        {
            if (IsZoneAlreadyInList(newZone))
                throw new ArgumentException("Already in list");

            _zones.Add(newZone);
        }

        public RouteList Clone()
        {
            return new RouteList(_zones);
        }
    }
}