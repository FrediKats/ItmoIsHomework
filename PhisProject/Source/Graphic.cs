using System;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace PhysProject.Source
{
    public class Graphic
    {
        #region Public
        public readonly PlotModel Model;

        public Graphic(PlotView plotView)
        {
            _plotView = plotView;
            Model = new PlotModel();

            _plotView.Model = Model;
            Model.Axes.Clear();
            CleanGraph();

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

        public void AddFunctionSeries(Func<double, double> f, double st, double end, double step)
        {
            Model.Series.Add(new FunctionSeries(f, st, end, step));
            UpdateModel();
        }

        public void AddPoint(int seriesNumber, double x, double y)
        {
            if (Model.Series[seriesNumber].GetType() == typeof(OxyPlot.Series.LineSeries))
            {
                ((OxyPlot.Series.LineSeries)Model.Series[seriesNumber]).Points.Add(new DataPoint(x, y));
                UpdateModel();
            } 
        }

        public void AddPoint(int seriesNumber, double y)
        {
            if (Model.Series[seriesNumber].GetType() == typeof(OxyPlot.Series.LineSeries))
            {
                _time += Config.TimePerTick;
                ((OxyPlot.Series.LineSeries)Model.Series[0]).Points.Add(new DataPoint(_time, y));
                UpdateModel();
            }
            
        }

        public void CleanGraph()
        {
            Model.Series.Clear();
            UpdateModel();
        }
        #endregion

        #region Private
        private PlotView _plotView;
        private OxyPlot.Axes.LinearAxis _axisY, _axisX;
        private int _time = 0;

        private void UpdateModel()
        {
            Model.InvalidatePlot(true);
        }
        #endregion
    }
}