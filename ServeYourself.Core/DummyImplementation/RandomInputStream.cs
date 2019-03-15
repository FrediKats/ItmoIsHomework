using System;
using System.Collections.Generic;
using ServeYourself.Core.Abstractions;

namespace ServeYourself.Core.DummyImplementation
{
    public class RandomInputStream : IInputStream
    {
        private Random _random = new Random();
        private readonly int _maxValue;
        private int _currentDelay;

        public RandomInputStream(int maxValue)
        {
            _maxValue = maxValue;

            _currentDelay = _random.Next(_maxValue);
        }

        public List<IClient> GenerateClientStream(int time)
        {
            var clients = new List<IClient>();

            _currentDelay -= time;
            if (_currentDelay <= 0)
            {
                _currentDelay = _random.Next(_maxValue);
                clients.Add(new DummyClient());
            }

            return clients;
        }
    }
}