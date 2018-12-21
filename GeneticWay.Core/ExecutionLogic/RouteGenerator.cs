using System.Collections.Generic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.ExecutionLogic
{
    public class RouteGenerator
    {
        public RouteGenerator(GamePolygon polygon)
        {
            Polygon = polygon;
        }

        public RouteGenerator(List<Zone> zones)
        {
            Polygon = new GamePolygon(zones);
        }

        public GamePolygon Polygon { get; set; }

    }
}