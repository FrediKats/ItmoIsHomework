using System;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.Workers
{
    public class DummyWorker : IWorker
    {
        private IVisitor _currentVisitor;
        private bool? _served = false;
        private readonly int _timeForServing;
        private int _currentClientTime;

        public DummyWorker(int timeForServing)
        {
            _timeForServing = timeForServing;
        }

        public bool IsAvailable()
        {
            return _currentVisitor == null;
        }

        public bool IsClientServed()
        {
            return _served == true;
        }

        public bool AddClient(IVisitor visitor)
        {
            if (_currentVisitor != null)
                throw new Exception();

            _currentVisitor = visitor;
            _currentClientTime = _timeForServing;
            _served = false;

            return true;
        }

        public void ApplyTime(int time)
        {
            _currentClientTime -= time;
            if (_currentClientTime < 0 && _currentVisitor != null)
                _served = true;
        }

        public IVisitor GetServedClient()
        {
            if (_currentVisitor == null || _served == false)
                throw new Exception();

            IVisitor visitor = _currentVisitor;
            _currentVisitor = null;
            _served = null;
            return visitor;
        }
    }
}