using System;
using System.Collections.Generic;
using GeneticWay.Core.Models;
using GeneticWay.Core.RoutingLogic;
using GeneticWay.Core.Tools;
using GeneticWay.Core.Vectorization;

namespace GeneticWay.Core.ExecutionLogic
{
    public class GamePolygon
    {
        public GamePolygon()
        {
            Zones = new List<Circle>();
        }

        public List<Circle> Zones { get; }
        public MovableObject LastSuccessfulObject { get; set; }

        public List<Coordinate> GetBestPath()
        {
            if (LastSuccessfulObject == null)
            {
                var zoneIterationPath = new ZoneIterationPath();
                
                //TODO: dirty hckas
                zoneIterationPath.Zones.Add(Zones[3]);
                zoneIterationPath.Zones.Add(Zones[2]);

                return RouteGenerator.BuildPath(zoneIterationPath);
            }

            return LastSuccessfulObject.VisitedPoints;
        }

        public bool MutateObjectPath()
        {
            MovableObject nextGenerationObject = MutationIteration();
            if (nextGenerationObject != null)
            {
                LastSuccessfulObject = nextGenerationObject;
                return true;
            }

            return false;
        }

        private MovableObject MutationIteration()
        {
            //TODO: add checker
            try
            {
                MovableObject result = AntiAliasing.TrySmooth(GetBestPath());
                foreach (Coordinate coordinate in result.VisitedPoints)
                {
                    if (IsCoordinateInZone(coordinate))
                        return null;

                }

                if (result.Velocity.GetLength() > Configuration.Epsilon)
                    return null;

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool IsCoordinateInZone(Coordinate coordinate)
        {
            foreach (Circle zone in Zones)
            {
                if (zone.IsInZone(coordinate))
                {
                    return true;
                }
            }

            return false;
        }
    }
}