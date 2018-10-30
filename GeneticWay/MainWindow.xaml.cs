using System.IO;
using System.Windows;
using GeneticWay.Core.Models;
using GeneticWay.Core.Services;
using GeneticWay.Ui;
using Newtonsoft.Json;

namespace GeneticWay
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SimulationManager _simManager;

        public MainWindow()
        {
            InitializeComponent();
            _simManager = new SimulationManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(CountInput.Text);
            _simManager.MakeIteration(count);
            PixelDrawer pd = new PixelDrawer(Drawer);
            pd.DrawPoints(_simManager.PeekReport.Coordinates, _simManager.PeekReport.Zones);

            MessageBox.Show($"{_simManager.PeekReport}");
        }
    }
}