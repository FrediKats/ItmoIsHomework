using System;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Update;
using VDS.RDF.Writing.Formatting;

namespace SmartSpark.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri TestGraphUri = new Uri("http://example.org/graph");
            InMemoryDataset ds = new InMemoryDataset(new TripleStore(), new Uri("http://mydefaultgraph.org"));
            SparqlUpdateParser sparqlparser = new SparqlUpdateParser();
            String updates = $"INSERT DATA {{ GRAPH <{TestGraphUri}> {{ <ex:subject> <ex:predicate> <ex:object> }} }};";
            SparqlUpdateCommandSet cmds = sparqlparser.ParseFromString(updates);

            LeviathanUpdateProcessor processor = new LeviathanUpdateProcessor(ds);
            processor.ProcessCommandSet(cmds);

            Print(ds);

        }

        public static void Print(InMemoryDataset ds)
        {
            NTriplesFormatter formatter = new NTriplesFormatter();
            foreach (Triple t in ds[new Uri("http://example.org/graph")].Triples)
            {
                Console.WriteLine(t.ToString(formatter));
            }

        }
    }
}
