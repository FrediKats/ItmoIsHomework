using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Update;

namespace SmartSpark.Core
{
    public class RdfQueryWrapper
    {
        private readonly InMemoryDataset _dataset;

        public RdfQueryWrapper(InMemoryDataset dataset)
        {
            _dataset = dataset;
        }
        
        public List<Triple> GetAll(Uri graph)
        {
            return _dataset[graph].Triples.ToList();
        }

        public void Create(Uri graph, string subject, string predicate, string obj)
        {
            var subjectNode = _dataset[graph].CreateLiteralNode(subject);
            var predicateNode = _dataset[graph].CreateLiteralNode(predicate);
            var objNod = _dataset[graph].CreateLiteralNode(obj);

            _dataset[graph].Assert(new Triple(subjectNode, predicateNode, objNod));
            
            //SparqlUpdateParser sparqlparser = new SparqlUpdateParser();
            //String updates = $"INSERT DATA {{ GRAPH <{graph}> {{ {subjectNode} {predicateNode} {objNod} }} }};";
            //SparqlUpdateCommandSet cmds = sparqlparser.ParseFromString(updates);

            //LeviathanUpdateProcessor processor = new LeviathanUpdateProcessor(_dataset);
            //processor.ProcessCommandSet(cmds);
        }
    }
}