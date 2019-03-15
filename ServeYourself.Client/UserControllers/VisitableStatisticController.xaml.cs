using System.Collections.Generic;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Wpf;
using ServeYourself.Core.DataContainers;
using ServeYourself.Core.VisitablePoints;

namespace ServeYourself.Client.UserControllers
{
    public partial class VisitableStatisticController : UserControl
    {
        private readonly IVisitable _visitable;

        private readonly List<DataPoint> _visitorList;
        private readonly List<DataPoint> _queueList;
        private readonly List<DataPoint> _handlingList;

        public VisitableStatisticController(IVisitable visitable)
        {
            _visitable = visitable;
            _visitorList = new List<DataPoint>();
            _queueList = new List<DataPoint>();
            _handlingList = new List<DataPoint>();

            var visitorSeries = new LineSeries() {ItemsSource = _visitorList};
            var queueSeries = new LineSeries() { ItemsSource = _queueList};
            var handlingSeries = new LineSeries { ItemsSource = _handlingList};

            InitializeComponent();

            MainChart.Series.Add(visitorSeries);
            MainChart.Series.Add(queueSeries);
            MainChart.Series.Add(handlingSeries);
            DataContext = this;
        }

        public void UpdateControl()
        {
            ShopStatistic statistic = _visitable.GetStatistic();

            AddPoint(_visitorList, statistic.VisitorCount);
            AddPoint(_queueList, statistic.InQueueClientCount);
            AddPoint(_handlingList, statistic.HandlingClients);

            MainChart.InvalidatePlot();
        }

        private static void AddPoint(List<DataPoint> list, int value)
        {

            list.Add(new DataPoint(list.Count, value));
        }
    }
}
