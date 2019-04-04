using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        public LoggerService(ListBox logList, Plot plot)
        {
            _logList = logList;
            _plot = plot;

            _dataPoints = Enumerable.Range(0, Config.BalancerHandlersCount).Select(i => new List<DataPoint>())
                .ToArray();
            foreach (List<DataPoint> dataPoints in _dataPoints)
            {
                plot.Series.Add(new LineSeries {ItemsSource = dataPoints});
            }
        }

        public void AddLog(string log)
        {
            Application.Current.Dispatcher.Invoke(() => UpdateLogList(log));
        }

        public void UpdatePlot(List<int> sizes)
        {
            for (var i = 0; i < sizes.Count; i++)
            {
                AddPoint(_dataPoints[i], sizes[i]);
            }

            Application.Current.Dispatcher.Invoke(() => _plot.InvalidatePlot());
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