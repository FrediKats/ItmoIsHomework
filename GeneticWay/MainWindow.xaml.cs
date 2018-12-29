using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GeneticWay.Core.Legacy;
using GeneticWay.Core.Models;
using GeneticWay.Core.RouteGenerating;
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

        //TODO: replace with polygon
        private readonly ZoneIterationPath _zoneIterationPath;
        private readonly SimulationManager _simManager;
        private int _minCount = int.MaxValue;
        private List<Coordinate> _coordinates;

        public MainWindow()
        {
            InitializeComponent();
            _pixelDrawer = new PixelDrawer(Drawer);
            _simManager = new SimulationManager();

            ProblemCondition settings = CrossSystemDataCasting.LoadSettings();
            Configuration.Setup(settings.MaxForce, settings.DeltaTime);

            _zoneIterationPath = new ZoneIterationPath();
            //_zoneIterationPath.Zones.Add(new Circle((0.2, 0.2), 0.05));
            //_zoneIterationPath.Zones.Add(new Circle((0.4, 0.6), 0.05));
            //_zoneIterationPath.Zones.Add(new Circle((0.5, 0.8), 0.05));
            //_zoneIterationPath.Zones.Add(new Circle((0.8, 0.9), 0.05));

            _zoneIterationPath.Zones.AddRange(settings.ValidCircles);
            _coordinates = RouteGenerator.BuildPath(_zoneIterationPath);

            PrintFiled(_coordinates, settings.ValidCircles);
        }

        private MovableObject ExecuteSimulation(List<Coordinate> path)
        {
            //TODO: remove checking
            try
            {
                MovableObject result = AntiAliasing.TrySmooth(path);
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
                MovableObject nextGenerationObject = ExecuteSimulation(_coordinates);
                if (nextGenerationObject != null)
                {
                    movableObject = nextGenerationObject;
                    _coordinates = nextGenerationObject.VisitedPoints.Select(x => x).ToList();
                }
            }

            if (movableObject == null)
            {
                MessageBox.Show("Move object is null");
                return;
            }

            PrintFiled(movableObject.VisitedPoints, _zoneIterationPath.Zones);

            MessageBox.Show($"Old: {_minCount}, New: {_coordinates.Count}");
            if (_coordinates.Count < _minCount)
            {
                _minCount = _coordinates.Count;
            }

            MessageBox.Show($"Final speed: {movableObject?.Velocity.GetLength() ?? -1}");
            UpdatePlot(movableObject);
            CrossSystemDataCasting.OutputDataSerialization(movableObject.ForceVector);
        }

        //TODO: remove, deprecated
        private void RunOldGeneticAlgorithm(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(CountInput.Text);
            _simManager.MakeIteration(count);

            SimReport report = _simManager.Reports.First();
            PrintFiled(report.Coordinates, _simManager.Zones);

            MessageBox.Show($"{report}");
        }

        private void PrintFiled(List<Coordinate> path, List<Circle> zones)
        {
            _pixelDrawer.PrintBackgroundWithBlack()
                .AddZones(zones)
                .AddPoints(path)
                .PrintPixels();
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