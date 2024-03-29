﻿using System;
using System.Windows;
using PhysicsSource.Core.Models;
using PhysProject.Malinin.Models;

namespace PhysProject.Malinin.Views
{
    /// <summary>
    ///     Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private ExecuteField _field;

        public MainView()
        {
            InitializeComponent();
            UpdateUserInterface();
        }

        public void CreateObjectTrigger(object sender, RoutedEventArgs e)
        {
            var v = double.Parse(TextBoxV.Text);
            var angelDirection = double.Parse(TextBoxA.Text) / 180 * Math.PI;

            var vx = v * Math.Cos(angelDirection);
            var vy = v * Math.Sin(angelDirection);

            var newBall = new BalistModel(20,
                new TwoDimensional(100, int.Parse(TextBoxH.Text)),
                new TwoDimensional(vx, vy),
                angelDirection);

            newBall.AssignOutputBox(CoordsTextBlock);
            _field.AddObject(newBall);

            var topChart = new BaseChart(this.TopChart, "time, ticks", "y");
            var topSeries = new SingleAxisSeries(topChart, "Y Position");
            var botChart = new BaseChart(BottomChart, "time, ticks", "v");
            var velocitySeries = new SingleAxisSeries(botChart, "Velocity");

            newBall.OnPositionChanged += (o, args) => { topSeries.AddPoint(args.CurrentPosition.Y); };
            newBall.OnSpeedChanged += (o, args) => { velocitySeries.AddPoint(args.CurrentPosition.Y); };
        }

        private void UpdateUserInterface()
        {
            _field = new ExecuteField(30, MainCanvas);
            ButtonStart.Click += (sender, args) => _field.Start();
        }
    }
}