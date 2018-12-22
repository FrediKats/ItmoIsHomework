using System;
using System.Collections.Generic;
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
            Path = vectorizationModel.MovableObject.VisitedPoints;
            return vectorizationModel.MovableObject;
        }

        public void PathMutation()
        {
            int index = Random.Next(Path.Count);
            Path.RemoveAt(index);
        }

        public List<Coordinate> Path { get; private set; }
    }
}
