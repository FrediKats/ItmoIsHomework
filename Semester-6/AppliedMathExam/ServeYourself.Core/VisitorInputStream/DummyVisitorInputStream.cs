using System.Collections.Generic;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.VisitorInputStream
{
    public class DummyVisitorInputStream : IVisitorInputStream
    {
        private readonly int _deltaForGeneration;
        private int _currentDelta;

        public DummyVisitorInputStream(int deltaForGeneration)
        {
            _deltaForGeneration = deltaForGeneration;
        }

        public List<IVisitor> GenerateClientStream(int time)
        {
            _currentDelta += time;
            var clients = new List<IVisitor>();

            while (_currentDelta >= _deltaForGeneration)
            {
                clients.Add(new DummyVisitor());
                _currentDelta -= _deltaForGeneration;
            }

            return clients;
        }
    }
}