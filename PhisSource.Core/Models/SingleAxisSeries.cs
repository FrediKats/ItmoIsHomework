namespace PhisSource.Core.Models
{
    public class SingleAxisSeries
    {
        private readonly BaseSeries _baseSeries;
        private int _pointCount = 0;

        public SingleAxisSeries(BaseChart chart, string title)
        {
            _baseSeries = new BaseSeries(chart, title);
        }

        public void DeleteChart()
        {
            _baseSeries.DeleteChart();
            _pointCount = 0;
        }

        public void AddPoint(double yValue)
        {
            _baseSeries.AddPoint(_pointCount, yValue);
            _pointCount++;
        }
    }
}