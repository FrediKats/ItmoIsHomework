using System;
using ServeYourself.Core.Abstractions;

namespace ServeYourself.Core.DummyImplementation
{
    public class DummyWorker : IWorker
    {
        private IClient _currentClient;
        private bool? _served = false;
        private readonly int _timeForServing;
        private int _currentClientTime;

        public DummyWorker(int timeForServing)
        {
            this._timeForServing = timeForServing;
        }

        public bool IsAvailable()
        {
            return _currentClient == null;
        }

        public bool IsClientServed()
        {
            return _served == true;
        }

        public bool AddClient(IClient client)
        {
            if (_currentClient != null)
                throw new Exception();

            _currentClient = client;
            _currentClientTime = _timeForServing;
            _served = false;

            return true;
        }

        public void ApplyTime(int time)
        {
            _currentClientTime -= time;
            if (_currentClientTime < 0 && _currentClient != null)
                _served = true;
        }

        public IClient GetServedClient()
        {
            if (_currentClient == null || _served == false)
                throw new Exception();

            IClient client = _currentClient;
            _currentClient = null;
            _served = null;
            return client;
        }
    }
}