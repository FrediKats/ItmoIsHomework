using System;
using VDS.RDF;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Writing.Formatting;

namespace SmartSpark.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri testGraphUri = new Uri("http://example.org/graph");
            InMemoryDataset ds = new InMemoryDataset(new TripleStore(), testGraphUri);
            
            var rdfQueryWrapper = new RdfQueryWrapper(ds);
            rdfQueryWrapper.Create(testGraphUri, "ex:subject", "ex:predicate", "ex:object");

            NTriplesFormatter formatter = new NTriplesFormatter();
            foreach (Triple t in rdfQueryWrapper.GetAll(testGraphUri))
            {
                Console.WriteLine(t.ToString(formatter));
            }
        }
    }
}
