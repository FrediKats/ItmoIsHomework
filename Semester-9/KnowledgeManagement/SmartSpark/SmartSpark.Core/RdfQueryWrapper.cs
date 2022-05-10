using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Query.Datasets;

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
            return _dataset[graph]
                .GetTriplesWithPredicate(_dataset[graph].CreateLiteralNode("say"))
                .ToList();
        }

        public void Create(Uri graph, string subject, string predicate, string obj)
        {
            var subjectNode = _dataset[graph].CreateLiteralNode(subject);
            var predicateNode = _dataset[graph].CreateLiteralNode(predicate);
            var objNod = _dataset[graph].CreateLiteralNode(obj);

            _dataset[graph].Assert(new Triple(subjectNode, predicateNode, objNod));
        }
    }
}