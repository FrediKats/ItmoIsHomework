using OxyPlot.Series;

namespace PhysProject.Core
{
    public class CustomSeries
    {
        private CustomChart _chart;
        private LineSeries _series;

        public CustomSeries(CustomChart chart)
        {
            _chart = chart;
            _series = new LineSeries();
            _chart.Model.Series.Add(_series);
        }

        public void DeleteChart()
        {
            _chart.Model.Series.Remove(_series);
        }

        public void AddPoint(double x, double y)
        {
            _chart.AddPoint(_series, x, y);
        }
    }
}