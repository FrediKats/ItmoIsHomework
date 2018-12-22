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
        private readonly RouteList routeList;
        private readonly AntiAliasing antiAliasing;

        public MainWindow()
        {
            InitializeComponent();
            _pixelDrawer = new PixelDrawer(Drawer);

            routeList = new RouteList();
            routeList.Zones.Add(new Circle((0.2, 0.2), 0.05));
            routeList.Zones.Add(new Circle((0.4, 0.6), 0.05));
            routeList.Zones.Add(new Circle((0.5, 0.8), 0.05));
            routeList.Zones.Add(new Circle((0.8, 0.9), 0.05));
            List<Coordinate> testingPath = RouteGenerator.BuildPath(routeList);

            //MovableObject movableObject = MovableObject.Create();
            //var vectorizationModel = new RouteVectorizationModel(movableObject);
            //vectorizationModel.ApplyVectorization(testingPath);

            antiAliasing = new AntiAliasing(testingPath);
        }

        private MovableObject Test()
        {
            antiAliasing.PathMutation();
            return antiAliasing.GenerateRoute();
        }

        private void RunAntiAliasing(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(CountInput.Text);

            for (int i = 0; i < count - 1; i++)
            {
                Test();
            }

            MovableObject movableObject = Test();

            _pixelDrawer.PrintBackgroundWithBlack()
                .AddZones(routeList.Zones)
                .AddPoints(movableObject.VisitedPoints)
                .PrintPixels();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(CountInput.Text);
            _simManager.MakeIteration(count);

            PixelDrawer pd = new PixelDrawer(Drawer);
            SimReport report = _simManager.Reports.First();

            pd.PrintBackgroundWithBlack()
                .AddPoints(report.Coordinates)
                .AddZones(_simManager.Zones)
                .PrintPixels();
            MessageBox.Show($"{report}");
        }

        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(CountInput.Text);

            _simManager.MakeIteration(1);
            int lastIterationCount;
            SimReport report;

            do
            {
                report = _simManager.Reports.First();
                lastIterationCount = report.IterationCount;
                _simManager.MakeIteration(count);

            } while (lastIterationCount == report.IterationCount);

            var pd = new PixelDrawer(Drawer);
            pd.PrintBackgroundWithBlack()
                .AddPoints(report.Coordinates)
                .AddZones(_simManager.Zones)
                .PrintPixels();

            MessageBox.Show($"{report}");
        }
    }
}