using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;
using GeneticWay.Core.RoutingLogic;
using GeneticWay.Core.Tools;
using GeneticWay.Core.Vectorization;
using GeneticWay.Ui;
using OxyPlot;
using OxyPlot.Wpf;

namespace GeneticWay
{
    public partial class MainWindow : Window
    {
        private readonly PixelDrawer _pixelDrawer;

        private int _minCount = int.MaxValue;
        private readonly GamePolygon _gamePolygon;

        public MainWindow()
        {
            InitializeComponent();
            _pixelDrawer = new PixelDrawer(Drawer);

            ProblemCondition settings = CrossSystemDataCasting.LoadSettings();
            Configuration.Setup(settings.MaxForce, settings.DeltaTime);

            _gamePolygon = new GamePolygon();
            _gamePolygon.Zones.AddRange(settings.ValidCircles);
            _gamePolygon.BuildPath();

            PrintFiled(_gamePolygon.BestPath, settings.ValidCircles);
        }

        private void RunAntiAliasing(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(CountInput.Text);

            for (var i = 0; i < count; i++)
            {
                _gamePolygon.MutateObjectPath();
            }

            if (_gamePolygon.LastSuccessfulObject == null)
            {
                MessageBox.Show("Move object is null");
                return;
            }

            PrintFiled(_gamePolygon.LastSuccessfulObject.VisitedPoints, _gamePolygon.Zones);

            MessageBox.Show($"Old: {_minCount}, New: {_gamePolygon.LastSuccessfulObject.VisitedPoints.Count}");
            if (_gamePolygon.LastSuccessfulObject.VisitedPoints.Count < _minCount)
            {
                _minCount = _gamePolygon.LastSuccessfulObject.VisitedPoints.Count;
            }

            MessageBox.Show($"Final speed: {_gamePolygon.LastSuccessfulObject.Velocity.GetLength()}");
            UpdatePlot(_gamePolygon.LastSuccessfulObject.VelocityVectors);
            CrossSystemDataCasting.OutputDataSerialization(_gamePolygon.LastSuccessfulObject.ForceVector);
        }

        //TODO: remove, deprecated
        //private void RunOldGeneticAlgorithm(object sender, RoutedEventArgs e)
        //{
        //    int count = int.Parse(CountInput.Text);
        //    _simManager.MakeIteration(count);

        //    SimReport report = _simManager.Reports.First();
        //    PrintFiled(report.Coordinates, _simManager.Zones);

        //    MessageBox.Show($"{report}");
        //}

        private void PrintFiled(List<Coordinate> path, List<Circle> zones)
        {
            _pixelDrawer.PrintBackgroundWithBlack()
                .AddZones(zones)
                .AddPoints(path)
                .PrintPixels();
        }

        private void UpdatePlot(List<Coordinate> velocity)
        {
            VelocityPlot.Series.Clear();

            var series = new LineSeries
            {
                ItemsSource = velocity.Select((v, i) => new DataPoint(i, v.GetLength()))
            };

            VelocityPlot.Series.Add(series);
            VelocityPlot.InvalidatePlot();
        }
    }
}