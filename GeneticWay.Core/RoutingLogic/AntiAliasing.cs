using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;
using GeneticWay.Core.Vectorization;

namespace GeneticWay.Core.RoutingLogic
{
    public static class AntiAliasing
    {
        private static readonly Random Random = new Random();

        public static MovableObject TrySmooth(List<Coordinate> path)
        {
            List<Coordinate> mutatedPath = CreateMutated(path);
            MovableObject movableObject = RouteVectorization.ApplyVectorization(mutatedPath);

            //TODO: check, probably a bug
            var result = new List<Coordinate> {(0, 0)};
            result.AddRange(movableObject.VisitedPoints.Where(p => p != (0, 0)));

            return movableObject;
        }

        public static List<Coordinate> CreateMutated(List<Coordinate> path)
        {
            List<Coordinate> instance = path.Select(x => x).ToList();
            instance.RemoveAt(Random.Next(instance.Count));
            return instance;
        }
    }
}