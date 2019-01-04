using System;
using System.Collections.Generic;
using GeneticWay.Core.Models;
using GeneticWay.Core.RouteGenerating;
using GeneticWay.Core.RoutingLogic;
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
        public List<Coordinate> BestPath { get; private set; }
        public MovableObject LastSuccessfulObject { get; set; }


        public void BuildPath()
        {
            var zoneIterationPath = new ZoneIterationPath();
            BestPath = RouteGenerator.BuildPath(zoneIterationPath);

        }

        public void MutateObjectPath()
        {
            MovableObject nextGenerationObject = MutationIteration();
            if (nextGenerationObject != null)
            {
                LastSuccessfulObject = nextGenerationObject;
            }
        }

        private MovableObject MutationIteration()
        {
            //TODO: add checker
            try
            {
                MovableObject result = AntiAliasing.TrySmooth(BestPath);
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