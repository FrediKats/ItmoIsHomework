using System.Collections.Generic;
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
    }
}