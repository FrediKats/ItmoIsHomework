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

        public MovableObject GenerateRoute()
        {
            var vectorizationModel = new RouteVectorizationModel(MovableObject.Create());
            vectorizationModel.ApplyVectorization(Path);

            Path = vectorizationModel.MovableObject.VisitedPoints.Select(x => x).ToList();

            return vectorizationModel.MovableObject;
        }

        public void PathMutation()
        {
            Path.RemoveAt(Random.Next(Path.Count));
        }

        public AntiAliasing CreateMutated()
        {
            var instance = new AntiAliasing(Path.Select(x => x).ToList());
            instance.PathMutation();
            return instance;
        }

        public List<Coordinate> Path { get; private set; }
    }
}
