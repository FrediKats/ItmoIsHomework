using System.IO;
using System.Windows;
using GeneticWay.Core.Models;
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
            var sim = JsonConvert.DeserializeObject<SimReport>(File.ReadAllText("../../../GeneticWay.TestConsole/bin/Debug/backup.json"));
            PixelDrawer pd = new PixelDrawer(Drawer);
            pd.DrawPoints(sim.Coordinates, sim.Zones);

        }
    }
}