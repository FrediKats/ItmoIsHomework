using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GeneticWay.Core.AntiAliasing;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Legacy;
using GeneticWay.Core.Models;
using GeneticWay.Core.RouteGenerating;
using GeneticWay.Core.Vectorization;
using GeneticWay.Ui;

namespace GeneticWay
{
    public partial class MainWindow : Window
    {
        private readonly SimulationManager _simManager;
        private readonly PixelDrawer _pixelDrawer;
        private readonly RouteList _routeList;
        private int _minCount = Int32.MaxValue;

        private AntiAliasing AntiAliasing { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            _pixelDrawer = new PixelDrawer(Drawer);

            _simManager = new SimulationManager();

            _routeList = new RouteList();
            _routeList.Zones.Add(new Circle((0.2, 0.2), 0.05));
            _routeList.Zones.Add(new Circle((0.4, 0.6), 0.05));
            _routeList.Zones.Add(new Circle((0.5, 0.8), 0.05));
            _routeList.Zones.Add(new Circle((0.8, 0.9), 0.05));
            List<Coordinate> testingPath = RouteGenerator.BuildPath(_routeList);

            AntiAliasing = new AntiAliasing(testingPath);
        }

        private MovableObject Test()
        {
            AntiAliasing newSimulation = AntiAliasing.CreateMutated();

            try
            {
                MovableObject result = newSimulation.GenerateRoute();
                AntiAliasing = newSimulation;
                return result;
            }
            catch (Exception e)
            {
                // ignored
            }

            return null;
        }

        private void RunAntiAliasing(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(CountInput.Text);
            MovableObject movableObject = null;

            for (int i = 0; i < count - 1; i++)
            {
                movableObject = Test() ?? movableObject;
            }

            movableObject = Test() ?? movableObject;
            MessageBox.Show($"Old: {_minCount}, New: {AntiAliasing.Path.Count}");
            if (AntiAliasing.Path.Count < _minCount)
            {
                _minCount = AntiAliasing.Path.Count;
            }

            if (movableObject != null)
            {
                _pixelDrawer.PrintBackgroundWithBlack()
                    .AddZones(_routeList.Zones)
                    .AddPoints(movableObject.VisitedPoints)
                    .PrintPixels();
            }
        }

        private void TestOldGenAlgo(object sender, RoutedEventArgs e)
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

        //private void TestOldGenAlgo(object sender, RoutedEventArgs e)
        //{
        //    int count = int.Parse(CountInput.Text) - 1;

        //    _simManager.MakeIteration(1);
        //    int lastIterationCount;
        //    SimReport report;

        //    do
        //    {
        //        report = _simManager.Reports.First();
        //        lastIterationCount = report.IterationCount;
        //        _simManager.MakeIteration(count);

        //    } while (lastIterationCount == report.IterationCount);

        //    var pd = new PixelDrawer(Drawer);
        //    pd.PrintBackgroundWithBlack()
        //        .AddPoints(report.Coordinates)
        //        .AddZones(_simManager.Zones)
        //        .PrintPixels();

        //    MessageBox.Show($"{report}");
        //}
    }
}