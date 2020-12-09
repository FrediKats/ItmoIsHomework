using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Update;
using VDS.RDF.Writing.Formatting;

namespace SmartSpark.Server
{
    
    public class RdfHandler
    {
        private readonly TripleStore _tripleStore;
        private readonly InMemoryDataset _dataset;
        private readonly Uri _testGraphUri;

        public RdfHandler()
        {
            _tripleStore = new TripleStore();
            _testGraphUri = new Uri("http://example.org/graph");
            _dataset = new InMemoryDataset(_tripleStore, _testGraphUri);

            var graph = new Graph();
            graph.BaseUri = _testGraphUri;
            _dataset.AddGraph(graph);
        }

        public List<Triple> GetAll()
        {
            NTriplesFormatter formatter = new NTriplesFormatter();
            return _dataset[new Uri("http://example.org/graph")].Triples.ToList();
        }

        public void Create(string subject, string predicate, string obj)
        {
            SparqlUpdateParser sparqlparser = new SparqlUpdateParser();
            String updates = $"INSERT DATA {{ GRAPH <{_testGraphUri}> {{ <{subject}> <{predicate}> <{obj}> }} }};";
            SparqlUpdateCommandSet cmds = sparqlparser.ParseFromString(updates);

            LeviathanUpdateProcessor processor = new LeviathanUpdateProcessor(_dataset);
            processor.ProcessCommandSet(cmds);
        }
    }
}