using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.RouteGenerating
{
    public class RouteList
    {
        public RouteList()
        {
            Zones = new List<Circle>();
        }

        private RouteList(IEnumerable<Circle> zones)
        {
            Zones = zones.Select(z => z).ToList();
        }

        public List<Circle> Zones { get; }

        public bool IsZoneAlreadyInList(Circle newZone)
        {
            return Zones.Any(z => z == newZone);
        }

        public void AddNew(Circle newZone)
        {
            if (IsZoneAlreadyInList(newZone))
            {
                throw new ArgumentException("Already in list");
            }

            Zones.Add(newZone);
        }

        public RouteList Clone()
        {
            return new RouteList(Zones);
        }
    }
}