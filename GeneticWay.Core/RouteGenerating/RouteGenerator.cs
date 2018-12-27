using System;
using System.Collections.Generic;
using System.Linq;
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

        public RouteGenerator(List<Circle> zones)
        {
            Polygon = new GamePolygon(zones);
        }

        public GamePolygon Polygon { get; }

        public List<RouteList> RecursiveSearch(RouteList routeList)
        {
            var foundRoutes = new List<RouteList>();

            foreach (Circle zone in Polygon.Zones)
            {
                if (routeList.IsZoneAlreadyInList(zone))
                {
                    continue;
                }

                if (IsCanConnectBySegment(routeList.Zones.Last(), zone) == false)
                {
                    continue;
                }

                RouteList newRoute = routeList.Clone();
                newRoute.AddNew(zone);
                List<RouteList> descendantRoutes = RecursiveSearch(newRoute);
                if (descendantRoutes?.Count > 0)
                {
                    foundRoutes.AddRange(descendantRoutes);
                }
            }

            Segment routeToEnd = BuildSegmentToEnd(routeList.Zones.Last());
            if (Polygon.IsCanCreateLine(routeToEnd))
            {
                foundRoutes.Add(routeList);
            }

            return foundRoutes;
        }

        private static Segment BuildSegmentToEnd(Circle? zone)
        {
            if (zone == null)
            {
                return Segment.Of((0, 0), (1, 1));
            }

            return MathComputing.BuildSegmentFromCircleToPoint(zone.Value, (1, 1));
        }

        private bool IsCanConnectBySegment(Circle? from, Circle to)
        {
            Segment connectingSegment;
            if (from == null)
            {
                connectingSegment = MathComputing.BuildSegmentFromPointToCircle((0, 0), to);
            }
            else
            {
                connectingSegment = MathComputing.BuildCirclesConnectingSegment(from.Value, to);
            }

            return Polygon.IsCanCreateLine(connectingSegment);
        }

        public static List<Coordinate> BuildPath(RouteList routeList)
        {
            var coordinatesInPath = new List<Coordinate>();

            if (routeList.Zones.Count == 0)
            {
                return Segment.Of((0, 0), (1, 1)).ToCoordinatesList();
            }

            Segment pathToFirstCircle = MathComputing.BuildSegmentFromPointToCircle((0, 0), routeList.Zones.First());
            coordinatesInPath.AddRange(pathToFirstCircle.ToCoordinatesList());

            Coordinate lastPosition = pathToFirstCircle.End;
            for (var i = 0; i < routeList.Zones.Count - 1; i++)
            {
                Circle from = routeList.Zones[i];
                Circle to = routeList.Zones[i + 1];
                Segment pathBetweenCircles = MathComputing.BuildCirclesConnectingSegment(from, to);
                coordinatesInPath.AddRange(BuildPathOnCircle(lastPosition, pathBetweenCircles.Start, from));
                coordinatesInPath.AddRange(pathBetweenCircles.ToCoordinatesList());
                lastPosition = pathBetweenCircles.End;
            }

            Circle lastZone = routeList.Zones.Last();
            Segment pathToExit = MathComputing.BuildSegmentFromCircleToPoint(lastZone, (1, 1));
            coordinatesInPath.AddRange(BuildPathOnCircle(lastPosition, pathToExit.Start, lastZone));
            coordinatesInPath.AddRange(pathToExit.ToCoordinatesList());

            return coordinatesInPath;
        }

        private static List<Coordinate> BuildPathOnCircle(Coordinate from, Coordinate to, Circle zone)
        {
            var coordinates = new List<Coordinate>();

            from = from - zone.Coordinate;
            to = to - zone.Coordinate;

            double angleFrom = Math.Atan2(from.Y, from.X);
            double angleTo = Math.Atan2(to.Y, to.X);

            //TODO: Add direction choosing
            for (double a = angleFrom; a <= angleTo; a += 0.001)
            {
                coordinates.Add(MathComputing.GetPointOnCircleCoordinate(zone.Radius, a, zone.Coordinate));
            }

            return coordinates;
        }
    }
}