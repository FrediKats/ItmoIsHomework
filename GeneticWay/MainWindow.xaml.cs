using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using GeneticWay.Models;
using GeneticWay.Tools;
using GeneticWay.Ui;
using Newtonsoft.Json;

namespace GeneticWay
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var sim = JsonConvert.DeserializeObject<SimReport>(File.ReadAllText("backup.json"));
            PixelDrawer pd = new PixelDrawer(Drawer);
            pd.DrawPoints(sim.Coordinates);

        }
    }
}