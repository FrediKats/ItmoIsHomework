using System.Collections.Generic;
using System.Windows.Controls;
using OxyPlot;
using ServeYourself.Client.Tools;

namespace ServeYourself.Client.UserControllers
{
    public partial class VisitableStatisticController : UserControl
    {
        public IList<DataPoint> Points { get; private set; }

        public VisitableStatisticController()
        {
            Points = new List<DataPoint>();

            InitializeComponent();
            DataContext = this;
        }

        public void GeneratePoint()
        {
            Points.Add(new DataPoint(Points.Count, RandomInstance.Random.Next(20)));
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GeneratePoint();
            MainChart.InvalidatePlot(true);
        }
    }
}
