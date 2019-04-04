using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AppliedMath.LoadBalancer.Models;
using OxyPlot;
using OxyPlot.Wpf;

namespace AppliedMath.LoadBalancer
{
    public partial class MainWindow : Window
    {
        private readonly QueueGenerator _queueGenerator;

        public MainWindow()
        {
            InitializeComponent();
            var logger = new Logger((s) =>
            {
                lock (LogList)
                {
                    LogList.Items.Add(s);
                    if (VisualTreeHelper.GetChildrenCount(LogList) > 0)
                    {
                        var border = (Border)VisualTreeHelper.GetChild(LogList, 0);
                        var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                        scrollViewer.ScrollToBottom();
                    }
                }
            });
            _queueGenerator = new QueueGenerator(logger);

            _dataPoints = Enumerable.Range(0, Models.LoadBalancer.Size).Select(i => new List<DataPoint>()).ToArray();
            foreach (List<DataPoint> dataPoints in _dataPoints)
            {
                MainChart.Series.Add(new LineSeries() {ItemsSource = dataPoints});
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _queueGenerator.Start();
            Task.Run(PlotUpdater);
        }

        private readonly List<DataPoint>[] _dataPoints;
        private void PlotUpdater()
        {
            while (true)
            {
                List<int> sizes = _queueGenerator.Balancer.GetSizes();
                for (int i = 0; i < sizes.Count; i++)
                {
                    AddPoint(_dataPoints[i], sizes[i]);
                }

                Application.Current.Dispatcher.Invoke(() => MainChart.InvalidatePlot());
                Thread.Sleep(500);
            }
        }

        private static void AddPoint(List<DataPoint> list, int value)
        {
            list.Add(new DataPoint(list.Count, value));
        }
    }
}
