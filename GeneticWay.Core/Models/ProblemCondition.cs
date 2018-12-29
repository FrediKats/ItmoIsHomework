using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GeneticWay.Core.Models
{
    public class ProblemCondition
    {
        public string Name { get; set; }
        [JsonProperty("dt")]
        public double DeltaTime { get; set; }
        [JsonProperty("Fmax")]
        public double MaxForce { get; set; }
        public List<InputCircleData> Circles { get; set; }
        public List<Circle> ValidCircles => Circles.Select(c => new Circle((c.X, c.Y), c.R)).ToList();
    }
}