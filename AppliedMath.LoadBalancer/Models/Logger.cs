using System;
using System.Windows;

namespace AppliedMath.LoadBalancer.Models
{
    public class Logger
    {
        private readonly Action<string> _action;
        public Logger(Action<string> action)
        {
            _action = action;
        }

        public void AddLog(string log)
        {
            Application.Current.Dispatcher.Invoke(() => _action(log));
        }
    }
}