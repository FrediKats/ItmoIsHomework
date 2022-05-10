using System;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using LinearAxis = OxyPlot.Axes.LinearAxis;
using LineSeries = OxyPlot.Series.LineSeries;

namespace PhysicsSource.Core.Models
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

            var axisX = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = xAxesName
            };
            Model.Axes.Add(axisX);

            var axisY = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = yAxesName
            };
            Model.Axes.Add(axisY);
        }

        public void AddChart(LineSeries series)
        {
            Model.Series.Add(series);
        }

        public void AddFunctionSeries(Func<double, double> f, double st, double end, double step)
        {
            Model.Series.Add(new FunctionSeries(f, st, end, step));
            UpdateModel();
        }

        public void AddPoint(LineSeries series, double x, double y)
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