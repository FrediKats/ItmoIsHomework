using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticWay.Core.Models
{
    public class ZoneIterationPath
    {
        public ZoneIterationPath()
        {
            Zones = new List<Circle>();
        }

        private ZoneIterationPath(IEnumerable<Circle> zones)
        {
            Zones = zones.Select(z => z).ToList();
        }

        public List<Circle> Zones { get; }

        public bool IsZoneAlreadyInList(Circle newZone)
        {
            return Zones.Any(z => z == newZone);
        }

        public ZoneIterationPath Clone()
        {
            return new ZoneIterationPath(Zones);
        }
    }
}