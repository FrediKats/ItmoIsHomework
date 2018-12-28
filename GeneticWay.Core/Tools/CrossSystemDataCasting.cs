using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using GeneticWay.Core.Models;
using Newtonsoft.Json;

namespace GeneticWay.Core.Tools
{
    public static class CrossSystemDataCasting
    {
        private const string OutputFileName = "out.txt";
        public static void OutputDataSerialization(List<Coordinate> data)
        {
            var nfi = new NumberFormatInfo {NumberDecimalSeparator = "."};

            string result = string.Join("\n", data.Select(c => $"{c.X.ToString(nfi)}, {c.Y.ToString(nfi)}"));
            File.WriteAllText(OutputFileName, result);
        }

        public static ProblemCondition LoadSettings()
        {
            string jsonText = File.ReadAllText("Tools/DataSample.json");
            var settings = JsonConvert.DeserializeObject<ProblemCondition>(jsonText);
            return settings;
        }
    }
}