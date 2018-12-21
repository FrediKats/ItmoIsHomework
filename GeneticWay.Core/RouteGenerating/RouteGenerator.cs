using System;
using System.Collections.Generic;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.RouteGenerating
{
    public class RouteGenerator
    {
        public RouteGenerator(GamePolygon polygon)
        {
            Polygon = polygon;
        }

        public GamePolygon Polygon { get; }

        public RouteGenerator(List<Zone> zones)
        {
            Polygon = new GamePolygon(zones);
        }

        public List<RouteList> RecursiveSearch(RouteList routeList)
        {
            var foundRoutes = new List<RouteList>();

            foreach (Zone zone in Polygon.Zones)
            {
                if (routeList.IsZoneAlreadyInList(zone))
                {
                    continue;
                }

                if (IsCanConnectBySegment(routeList.LastZone, zone) == false)
                {
                    continue;
                }

                RouteList newRoute = routeList.Clone();
                newRoute.AddNew(zone);
                List<RouteList> descendantRoutes = RecursiveSearch(newRoute);
                if (descendantRoutes?.Count > 0)
                    foundRoutes.AddRange(descendantRoutes);
            }

            Segment routeToEnd = BuildSegmentToEnd(routeList.LastZone);
            if (Polygon.IsCanCreateLine(routeToEnd))
                foundRoutes.Add(routeList);

            return foundRoutes;
        }

        private static Segment BuildZonesConnectingSegment(Zone from, Zone to)
        {
            if ((from.Coordinate - to.Coordinate).GetLength() < (from.R + to.R))
                throw new ArgumentException("Zones are intersect");

            Coordinate closestPointFrom = from.GetClosestToPointCoordinate(to.Coordinate);
            Coordinate closestPointTo = to.GetClosestToPointCoordinate(from.Coordinate);

            return new Segment(closestPointFrom, closestPointTo);
        }

        private static Segment BuildSegmentToFirstZone(Zone zone)
        {

            Coordinate closestPoint = zone.GetClosestToPointCoordinate((0, 0));
            return new Segment((0, 0), closestPoint);
        }

        private static Segment BuildSegmentToEnd(Zone? zone)
        {
            if (zone == null)
                return new Segment((0, 0), (1, 1));

            Coordinate closestPoint = zone.Value.GetClosestToPointCoordinate((1, 1));
            return new Segment(closestPoint, zone.Value.Coordinate);
        }

        private bool IsCanConnectBySegment(Zone? from, Zone to)
        {
            Segment connectingSegment;
            if (from == null)
            {
                connectingSegment = BuildSegmentToFirstZone(to);
            }
            else
            {
                connectingSegment = BuildZonesConnectingSegment(from.Value, to);
            }

            return Polygon.IsCanCreateLine(connectingSegment);
        }

        private static List<Coordinate> BuildPathOnCircle(Coordinate from, Coordinate to, Zone zone)
        {
            var coordinates = new List<Coordinate>();

            from = from - zone.Coordinate;
            to = to - zone.Coordinate;

            double angleFrom = Math.Asin(from.Y / zone.R);
            double angleTo = Math.Asin(to.Y / zone.R);

            //TODO: Add direction choosing

            for (double a = angleFrom; a <= angleTo; a += 0.01)
            {

            }

            return coordinates;
        }
    }
}