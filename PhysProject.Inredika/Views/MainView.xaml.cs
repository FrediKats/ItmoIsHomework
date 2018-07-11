using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using OxyPlot;
using PhysicsSource.Core.Models;
using PhysProject.Inredika.Models;

namespace PhysProject.Inredika.Views
{
    /// <summary>
    ///     Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private ExecuteField _field;

        private SpringModel _spring;

        private List<TickerModel> _tickerList = new List<TickerModel>();
        public BaseChart ChartTop, ChartBot;

        public MainView()
        {
            InitializeComponent();
            InitialCanvas();

            ButtonCreateSpring.Click += GetSpringData;
            ButtonCreateTicker.Click += GetTickerData;
            ButtonStart.Click += (sender, args) => _field.Start();
            ButtonClean.Click += CleanCanvas;
        }

        public void GetSpringData(object sender, EventArgs e)
        {
            var mass = double.Parse(InputSpringMass.Text);
            var coefK = double.Parse(InputSpringCoefK.Text);
            var deltaX = double.Parse(InputSpringDeltaY.Text);
            var coefC = double.Parse(InputSpringCoefC.Text);

            StartSpring(mass, coefK, deltaX, coefC);
        }

        public void GetTickerData(object sender, EventArgs e)
        {
            var length = double.Parse(InputTickerLength.Text);
            var angle = double.Parse(InputTickerAngle.Text);

            StartTicker(length, angle);
        }

        public void CleanCanvas(object sender, EventArgs e)
        {
            foreach (var ticker in _tickerList) ticker.DeleteTicker();
            _tickerList = new List<TickerModel>();
            _spring?.DeleteSpring();
            _spring = null;
            _field.Stop();
        }

        private void InitialCanvas()
        {
            _field = new ExecuteField(20, InterfaceCanvas);
            var border = new Line
            {
                Name = "border",
                StrokeThickness = 2,
                Stroke = Brushes.Navy,
                X2 = 600
            };
            _field.FieldCanvas.Children.Add(border);
            var lineTracker = new Line
            {
                Name = "border",
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                Y1 = 150,
                Y2 = 150,
                X2 = 600
            };
            _field.FieldCanvas.Children.Add(lineTracker);
            var lineTrackerSec = new Line
            {
                Name = "border",
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                Y1 = 100,
                Y2 = 100,
                X2 = 600
            };
            _field.FieldCanvas.Children.Add(lineTrackerSec);


            ChartTop = new BaseChart(PlotViewTop, "Время, мс", "");
            ChartBot = new BaseChart(PlotViewBot, "Время, мс", "");

            ChartBot.Model.DefaultColors[0] = OxyColors.Magenta;
            ChartBot.Model.DefaultColors[1] = OxyColors.Navy;

            ChartTop.Model.DefaultColors[0] = OxyColors.Magenta;
            ChartTop.Model.DefaultColors[1] = OxyColors.Navy;
        }

        private void StartSpring(double mass, double coefK, double deltaX, double coefC)
        {
            if (_spring != null) _spring.DeleteSpring();
            _spring = new SpringModel(_field, new TwoDimensional(300, 100), mass, coefK, deltaX, coefC);
            _field.AddObject(_spring);
            //_spring.SeriesList.Add(new CustomSeries(ChartTop));
            //_spring.SeriesList.Add(new CustomSeries(ChartBot));
            //_spring.SeriesList[0]._series.Title = "Координата Y";
            //_spring.SeriesList[1]._series.Title = "Скорость по Y";
        }

        private void StartTicker(double length, double angle)
        {
            var _ticker = new TickerModel(_field, new TwoDimensional(150, 0), length, angle);
            _field.AddObject(_ticker);
            _tickerList.Add(_ticker);
            //_ticker.SeriesList.Add(new CustomSeries(ChartTop));
            //_ticker.SeriesList.Add(new CustomSeries(ChartBot));

            //_ticker.SeriesList[0]._series.Title = "Координата X";
            //_ticker.SeriesList[1]._series.Title = "Скорость по X";
        }
    }
}