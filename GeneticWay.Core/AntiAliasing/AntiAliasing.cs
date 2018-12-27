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
            List<Coordinate> result = new List<Coordinate>();
            result.Add((0,0));
            foreach (Coordinate position in vectorizationModel.MovableObject.VisitedPoints)
            {
                if (position.X != 0 && position.Y != 0)
                {
                    result.Add(position);
                }
            }
            Path = result;

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
