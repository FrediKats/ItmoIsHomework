﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;
using GeneticWay.Core.Services;
using GeneticWay.Ui;

namespace GeneticWay
{
    public partial class MainWindow : Window
    {
        private readonly SimulationManager _simManager;
        private PixelDrawer _pixelDrawer;

        public MainWindow()
        {
            InitializeComponent();
            _pixelDrawer = new PixelDrawer(Drawer);

            _pixelDrawer.PrintBackgroundWithBlack()
                .AddSegments(new List<Segment>() {new Segment((0, 0), (0.3, 0.6))})
                .AddZones(new List<Zone>() {new Zone((0.4, 0.4), 0.2)})
                .PrintPixels();

            //_simManager = new SimulationManager();
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