using System;
using System.Collections.Generic;
using SmartSpark.Core;
using VDS.RDF;
using VDS.RDF.Query.Datasets;

namespace SmartSpark.Server
{
    public class RdfHandler
    {
        private readonly RdfQueryWrapper _rdfQueryWrapper;
        private readonly Uri _testGraphUri;

        public RdfHandler()
        {
            _testGraphUri = new Uri("http://example.org/graph");
            var dataset = new InMemoryDataset(new TripleStore(), _testGraphUri);
            dataset.AddGraph(new Graph { BaseUri = _testGraphUri });

            _rdfQueryWrapper = new RdfQueryWrapper(dataset);
        }

        public List<Triple> GetAll()
        {
            return _rdfQueryWrapper.GetAll(_testGraphUri);
        }

        public void Create(string subject, string predicate, string obj)
        {
            _rdfQueryWrapper.Create(_testGraphUri, subject, predicate, obj);
        }
    }
}