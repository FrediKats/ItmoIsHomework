using System.Collections.Generic;
using ServeYourself.Core.Abstractions;

namespace ServeYourself.Core.DummyImplementation
{
    public class DummyInputStream : IInputStream
    {
        private readonly int _deltaForGeneration;
        private int _currentDelta;

        public DummyInputStream(int deltaForGeneration)
        {
            _deltaForGeneration = deltaForGeneration;
        }

        public List<IClient> GenerateClientStream(int time)
        {
            _currentDelta += time;
            var clients = new List<IClient>();

            while (_currentDelta >= _deltaForGeneration)
            {
                clients.Add(new DummyClient());
                _currentDelta -= _deltaForGeneration;
            }

            return clients;
        }
    }
}