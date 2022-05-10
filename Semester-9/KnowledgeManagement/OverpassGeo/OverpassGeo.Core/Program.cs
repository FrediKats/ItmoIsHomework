using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OsmSharp;
using OsmSharp.Streams;

namespace OverpassGeo.Core
{
    public static class Download
    {
        /// <summary>
        /// Downloads a file if it doesn't exist yet.
        /// </summary>
        public static async Task ToFile(string url, string filename)
        {
            if (!File.Exists(filename))
            {
                var client = new HttpClient();
                using (var stream = await client.GetStreamAsync(url))
                using (var outputStream = File.OpenWrite(filename))
                {
                    stream.CopyTo(outputStream);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Download
                .ToFile("https://overpass-api.de/api/interpreter?data=%5Bout%3Ajson%5D%3B%0A%28%20area%5Bname%3D%22%D0%A1%D0%B0%D0%BD%D0%BA%D1%82-%D0%9F%D0%B5%D1%82%D0%B5%D1%80%D0%B1%D1%83%D1%80%D0%B3%22%5D%3B%20%29-%3E.searchArea%3B%0A%0A%28%0A%20%20node%5B%22tourism%22%3D%22attraction%22%5D%28area.searchArea%29%3B%0A%20%20node%5B%22tourism%22%3D%22museum%22%5D%28area.searchArea%29%3B%0A%20%20node%5B%22tourism%22%3D%22viewpoint%22%5D%28area.searchArea%29%3B%0A%29%3B%0A%0A%0A%28._%3B%3E%3B%29%3B%0Aout%20body%3B", "data.pbf")
                .Wait();
            var readAllText = File.ReadAllText("data.pbf");
            var jsonDocument = JsonDocument.Parse(readAllText);
            var jsonElement = jsonDocument.RootElement.GetProperty("elements");
            Console.WriteLine(jsonElement);
            using (var fileStream = File.OpenRead("data.pbf"))
            {
                var source = new PBFOsmStreamSource(fileStream); // create source stream.
                List<OsmGeo> osmGeos = source.ToList();
                foreach (var osmGeo in osmGeos)
                {
                    Console.WriteLine(osmGeo.ToString());

                }
            }
        }
    }
}
