using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;
using GeneticWay.Core.Vectorization;

namespace GeneticWay.Core.AntiAliasing
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
            var vectorizationModel = new RouteVectorizationModel(MovableObject.Create());
            vectorizationModel.ApplyVectorization(Path);

            var result = new List<Coordinate> {(0, 0)};
            result.AddRange(vectorizationModel.MovableObject.VisitedPoints.Where(p => p != (0, 0)));

            Path = result;
            return vectorizationModel.MovableObject;
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