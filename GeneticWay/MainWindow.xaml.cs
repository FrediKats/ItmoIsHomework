using System.Linq;
using System.Windows;
using GeneticWay.Core.Models;
using GeneticWay.Core.Services;
using GeneticWay.Ui;

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
            SimReport report = _simManager.Reports.First();
            pd.DrawPoints(report.Coordinates, _simManager.Zones);
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
            pd.DrawPoints(report.Coordinates, _simManager.Zones);
            MessageBox.Show($"{report}");
        }
    }
}