using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GeneticWay.Core.Legacy;
using GeneticWay.Core.Models;
using GeneticWay.Core.RouteGenerating;
using GeneticWay.Core.RoutingLogic;
using GeneticWay.Core.Vectorization;
using GeneticWay.Ui;
using OxyPlot;
using OxyPlot.Wpf;

namespace GeneticWay
{
    public partial class MainWindow : Window
    {
        private readonly PixelDrawer _pixelDrawer;

        //TODO: replace with polygon
        private readonly ZoneIterationPath _zoneIterationPath;
        private readonly SimulationManager _simManager;
        private int _minCount = int.MaxValue;


        public MainWindow()
        {
            InitializeComponent();
            _pixelDrawer = new PixelDrawer(Drawer);
            _simManager = new SimulationManager();

            _zoneIterationPath = new ZoneIterationPath();
            _zoneIterationPath.Zones.Add(new Circle((0.2, 0.2), 0.05));
            _zoneIterationPath.Zones.Add(new Circle((0.4, 0.6), 0.05));
            _zoneIterationPath.Zones.Add(new Circle((0.5, 0.8), 0.05));
            _zoneIterationPath.Zones.Add(new Circle((0.8, 0.9), 0.05));
            List<Coordinate> testingPath = RouteGenerator.BuildPath(_zoneIterationPath);

            AntiAliasing = new AntiAliasing(testingPath);
        }

        private AntiAliasing AntiAliasing { get; set; }

        private MovableObject ExecuteSimulation()
        {
            AntiAliasing newSimulation = AntiAliasing.CreateMutated();

            //TODO: remove checking
            try
            {
                MovableObject result = newSimulation.GenerateRoute();
                AntiAliasing = newSimulation;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void RunAntiAliasing(object sender, RoutedEventArgs e)
        {
            MovableObject movableObject = null;
            int count = int.Parse(CountInput.Text);

            for (var i = 0; i < count; i++)
            {
                movableObject = ExecuteSimulation() ?? movableObject;
            }

            if (movableObject == null)
            {
                MessageBox.Show("Move object is null");
                return;
            }

            _pixelDrawer.PrintBackgroundWithBlack()
                .AddZones(_zoneIterationPath.Zones)
                .AddPoints(movableObject.VisitedPoints)
                .PrintPixels();

            MessageBox.Show($"Old: {_minCount}, New: {AntiAliasing.Path.Count}");
            if (AntiAliasing.Path.Count < _minCount)
            {
                _minCount = AntiAliasing.Path.Count;
            }

            MessageBox.Show($"Final speed: {movableObject?.Velocity.GetLength() ?? -1}");
            UpdatePlot(movableObject);
        }

        //TODO: remove, deprecated
        private void RunOldGeneticAlgorithm(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(CountInput.Text);
            _simManager.MakeIteration(count);

            SimReport report = _simManager.Reports.First();

            _pixelDrawer.PrintBackgroundWithBlack()
                .AddPoints(report.Coordinates)
                .AddZones(_simManager.Zones)
                .PrintPixels();

            MessageBox.Show($"{report}");
        }

        private void UpdatePlot(MovableObject movableObject)
        {
            VelocityPlot.Series.Clear();

            var series = new LineSeries
            {
                ItemsSource = movableObject.VelocityVectors.Select((v, i) => new DataPoint(i, v.GetLength()))
            };

            VelocityPlot.Series.Add(series);
            VelocityPlot.InvalidatePlot();
        }
    }
}