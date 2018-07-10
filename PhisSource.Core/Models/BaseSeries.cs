using OxyPlot.Series;

namespace PhisSource.Core.Models
{
    public class BaseSeries
    {
        private readonly BaseChart _chart;
        private readonly LineSeries _series;

        public BaseSeries(BaseChart chart, string title)
        {
            _chart = chart;
            _series = new LineSeries {Title = title};
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