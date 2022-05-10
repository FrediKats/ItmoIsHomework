using System.Collections.Generic;
using ServeYourself.Core.Tools;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.VisitorInputStream
{
    public class RandomVisitorInputStream : IVisitorInputStream
    {
        private readonly int _maxValue;
        private int _currentDelay;

        public RandomVisitorInputStream(int maxValue)
        {
            _maxValue = maxValue;

            _currentDelay = RandomProvider.Random.Next(_maxValue);
        }

        public List<IVisitor> GenerateClientStream(int time)
        {
            var clients = new List<IVisitor>();

            _currentDelay -= time;
            if (_currentDelay <= 0)
            {
                _currentDelay = RandomProvider.Random.Next(_maxValue);
                clients.Add(new DummyVisitor());
            }

            return clients;
        }
    }
}