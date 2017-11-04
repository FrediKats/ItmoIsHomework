using System;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace PhysProject.Core
{
    public class CustomChart
    {
        #region Public
        public readonly PlotModel Model;

        public CustomChart(PlotView plotView)
        {
            _plotView = plotView;
            Model = new PlotModel();

            _plotView.Model = Model;
            Model.Axes.Clear();
            CleanChart();

            _axisX = new OxyPlot.Axes.LinearAxis()
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom
            };
            _axisY = new OxyPlot.Axes.LinearAxis()
            {
                Position = OxyPlot.Axes.AxisPosition.Left
            };
            Model.Axes.Add(_axisX);
            Model.Axes.Add(_axisY);
        }

        public void SetAxisTitle(string nameX, string nameY)
        {
            _axisX.Title = nameX;
            _axisY.Title = nameY;
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
        #endregion

        #region Private
        private PlotView _plotView;
        private OxyPlot.Axes.LinearAxis _axisY, _axisX;
        #endregion
    }
}