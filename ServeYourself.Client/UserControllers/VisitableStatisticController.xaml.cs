using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Wpf;
using ServeYourself.Core.Abstractions;
using ServeYourself.Core.DataContainers;

namespace ServeYourself.Client.UserControllers
{
    public partial class VisitableStatisticController : UserControl
    {
        private readonly IVisitable _visitable;

        private readonly LineSeries _visitorSeries;
        private readonly LineSeries _queueSeries;
        private readonly LineSeries _handlingSeries;

        private readonly List<DataPoint> _visitorList;
        private readonly List<DataPoint> _queueList;
        private readonly List<DataPoint> _handlingList;

        public VisitableStatisticController(IVisitable visitable)
        {
            _visitable = visitable;
            _visitorList = new List<DataPoint>();
            _queueList = new List<DataPoint>();
            _handlingList = new List<DataPoint>();

            _visitorSeries = new LineSeries() {ItemsSource = _visitorList};
            _queueSeries = new LineSeries() { ItemsSource = _queueList};
            _handlingSeries = new LineSeries { ItemsSource = _handlingList};

            InitializeComponent();

            MainChart.Series.Add(_visitorSeries);
            MainChart.Series.Add(_queueSeries);
            MainChart.Series.Add(_handlingSeries);
            DataContext = this;
        }

        public void UpdateControl()
        {
            ShopStatistic statistic = _visitable.GetStatistic();

            AddPoint(_visitorList, statistic.VisitorCount);
            AddPoint(_queueList, statistic.InQueueClientCount);
            AddPoint(_handlingList, statistic.HandlingClients);

            MainChart.InvalidatePlot(true);
        }

        public static void AddPoint(List<DataPoint> list, int value)
        {

            list.Add(new DataPoint(list.Count, value));
        }
    }
}
