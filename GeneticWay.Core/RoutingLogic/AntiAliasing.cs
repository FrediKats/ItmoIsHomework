using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;
using GeneticWay.Core.Vectorization;

namespace GeneticWay.Core.RoutingLogic
{
    public class AntiAliasing
    {
        private static readonly Random Random = new Random();

        public AntiAliasing(List<Coordinate> path)
        {
            Path = path;
        }

        public List<Coordinate> Path { get; private set; }

        public MovableObject GenerateRoute()
        {
            MovableObject movableObject = RouteVectorization.ApplyVectorization(Path);

            var result = new List<Coordinate> {(0, 0)};
            result.AddRange(movableObject.VisitedPoints.Where(p => p != (0, 0)));

            Path = result;
            return movableObject;
        }

        private void PathMutation()
        {
            //int countToRemove = Path.Count / 100;
            //for (int i = 0; i < countToRemove; i++)
            //{
            Path.RemoveAt(Random.Next(Path.Count));
            //}
        }

        public AntiAliasing CreateMutated()
        {
            var instance = new AntiAliasing(Path.Select(x => x).ToList());
            instance.PathMutation();
            return instance;
        }
    }
}