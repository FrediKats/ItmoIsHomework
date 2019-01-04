using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;
using GeneticWay.Core.RouteGenerating;
using GeneticWay.Core.Tools;

namespace GeneticWay.Core.RoutingLogic
{
    public class RouteGenerator
    {
        public RouteGenerator(GamePolygon polygon)
        {
            Polygon = polygon;
        }

        public GamePolygon Polygon { get; }

        public List<ZoneIterationPath> RecursiveSearch(ZoneIterationPath zoneIterationPath)
        {
            var foundRoutes = new List<ZoneIterationPath>();

            foreach (Circle zone in Polygon.Zones)
            {
                if (zoneIterationPath.IsZoneAlreadyInList(zone))
                {
                    continue;
                }

                if (IsCanConnectBySegment(zoneIterationPath.Zones.Last(), zone) == false)
                {
                    continue;
                }

                ZoneIterationPath newRoute = zoneIterationPath.Clone();
                newRoute.AddNew(zone);
                List<ZoneIterationPath> descendantRoutes = RecursiveSearch(newRoute);
                if (descendantRoutes?.Count > 0)
                {
                    foundRoutes.AddRange(descendantRoutes);
                }
            }

            Segment routeToEnd = BuildSegmentToEnd(zoneIterationPath.Zones.Last());
            if (IsCanCreateLine(routeToEnd))
            {
                foundRoutes.Add(zoneIterationPath);
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

            return IsCanCreateLine(connectingSegment);
        }

        public bool IsCanCreateLine(Segment segment)
        {
            foreach (Circle zone in Polygon.Zones)
            {
                if (CircleAndLineIntersection(segment, zone))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CircleAndLineIntersection(Segment segment, Circle circle)
        {
            Segment otherCoordinateSystemSegment = segment - circle.Coordinate;
            Coordinate closestPoint = otherCoordinateSystemSegment.GetSegmentClosestToCenterPoint();

            return closestPoint.GetLength() <= circle.Radius;
        }

        public static List<Coordinate> BuildPath(ZoneIterationPath zoneIterationPath)
        {
            var coordinatesInPath = new List<Coordinate>();

            if (zoneIterationPath.Zones.Count == 0)
            {
                return Segment.Of((0, 0), (1, 1)).ToCoordinatesList();
            }

            Segment pathToFirstCircle = MathComputing.BuildSegmentFromPointToCircle((0, 0), zoneIterationPath.Zones.First());
            coordinatesInPath.AddRange(pathToFirstCircle.ToCoordinatesList());

            Coordinate lastPosition = pathToFirstCircle.End;
            for (var i = 0; i < zoneIterationPath.Zones.Count - 1; i++)
            {
                Circle from = zoneIterationPath.Zones[i];
                Circle to = zoneIterationPath.Zones[i + 1];
                Segment pathBetweenCircles = MathComputing.BuildCirclesConnectingSegment(from, to);
                coordinatesInPath.AddRange(BuildPathOnCircle(lastPosition, pathBetweenCircles.Start, from));
                coordinatesInPath.AddRange(pathBetweenCircles.ToCoordinatesList());
                lastPosition = pathBetweenCircles.End;
            }

            Circle lastZone = zoneIterationPath.Zones.Last();
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

            double angleFrom = (Math.Atan2(from.Y, from.X) + 2 * Math.PI) % (2 * Math.PI);
            double angleTo = (Math.Atan2(to.Y, to.X) + 2 * Math.PI) % (2 * Math.PI);

            if ((angleTo + 2 * Math.PI - angleFrom) % (2 * Math.PI) > Math.PI)
            {
                if (angleTo > angleFrom)
                {
                    angleFrom += (Math.PI * 2);
                }

                for (double a = angleFrom; a >= angleTo; a -= Configuration.Epsilon)
                {
                    coordinates.Add(MathComputing.GetPointOnCircleCoordinate(zone.Radius, a, zone.Coordinate));
                }
            }
            else
            {
                if (angleTo < angleFrom)
                {
                    angleTo += (Math.PI * 2);
                }

                for (double a = angleFrom; a <= angleTo; a += Configuration.Epsilon)
                {
                    coordinates.Add(MathComputing.GetPointOnCircleCoordinate(zone.Radius, a, zone.Coordinate));
                }
            }

            return coordinates;
        }
    }
}