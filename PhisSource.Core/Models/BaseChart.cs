using System;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace PhisSource.Core.Models
{
    public class BaseChart
    {
        public readonly PlotModel Model;

        public BaseChart(PlotView plotView, string xAxesName, string yAxesName)
        {
            Model = new PlotModel();
            plotView.Model = Model;

            Model.Axes.Clear();
            CleanChart();

            var axisX = new OxyPlot.Axes.LinearAxis()
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Title = xAxesName
            };
            Model.Axes.Add(axisX);

            var axisY = new OxyPlot.Axes.LinearAxis()
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Title = yAxesName
            };
            Model.Axes.Add(axisY);
        }
        public void AddChart(OxyPlot.Series.LineSeries series)
        {
            Model.Series.Add(series);
        }

        public void AddFunctionSeries(Func<double, double> f, double st, double end, double step)
        {
            Model.Series.Add(new FunctionSeries(f, st, end, step));
            UpdateModel();
        }

        public void AddPoint(OxyPlot.Series.LineSeries series, double x, double y)
        {
            series.Points.Add(new DataPoint(x, y));
            UpdateModel();
        }

        public void CleanChart()
        {
            Model.Series.Clear();
            UpdateModel();
        }

        public void UpdateModel()
        {
            Model.InvalidatePlot(true);
        }

    }
}