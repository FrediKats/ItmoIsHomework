using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AppliedMath.LoadBalancer.Models;
using AppliedMath.LoadBalancer.Tools;
using OxyPlot;
using OxyPlot.Wpf;

namespace AppliedMath.LoadBalancer.Services
{
    public class LoggerService
    {
        private readonly List<DataPoint>[] _dataPoints;

        private readonly ListBox _logList;
        private readonly Plot _plot;
        private readonly TextBlock _statusOutput;

        public LoggerService(ListBox logList, Plot plot, TextBlock statusOutput)
        {
            _logList = logList;
            _plot = plot;
            _statusOutput = statusOutput;

            _dataPoints = Enumerable
                .Range(0, Config.BalancerHandlersCount)
                .Select(i => new List<DataPoint>())
                .ToArray();
            foreach (List<DataPoint> dataPoints in _dataPoints)
            {
                plot.Series.Add(new LineSeries {ItemsSource = dataPoints, Title = $"Worker-{plot.Series.Count + 1}"});
            }
        }

        public void AddLog(string log)
        {
            Application.Current.Dispatcher.Invoke(() => UpdateLogList(log));
        }

        public void UpdatePlot(List<HandlerStateInfo> statuses)
        {
            for (var i = 0; i < statuses.Count; i++)
            {
                AddPoint(_dataPoints[i], statuses[i].QueueSize);
            }

            Application.Current.Dispatcher.Invoke(() => _plot.InvalidatePlot());

            string info = string.Join("\n",
                statuses.Select(s => $"Worker-{s.WorkerId} loads for: {s.LoadingPercent:P}"));
            Application.Current.Dispatcher.Invoke(() => _statusOutput.Text = info);
            Thread.Sleep(500);
        }

        private void UpdateLogList(string log)
        {
            lock (_logList)
            {
                _logList.Items.Add(log);
                if (VisualTreeHelper.GetChildrenCount(_logList) > 0)
                {
                    var border = (Border) VisualTreeHelper.GetChild(_logList, 0);
                    var scrollViewer = (ScrollViewer) VisualTreeHelper.GetChild(border, 0);
                    scrollViewer.ScrollToBottom();
                }
            }
        }

        private static void AddPoint(List<DataPoint> list, int value)
        {
            list.Add(new DataPoint(list.Count, value));
        }
    }
}