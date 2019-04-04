using System.Threading.Tasks;
using System.Windows;
using AppliedMath.LoadBalancer.Services;

namespace AppliedMath.LoadBalancer
{
    public partial class MainWindow : Window
    {
        private readonly InputStreamService _inputStreamService;
        private readonly LoggerService _logger;

        public MainWindow()
        {
            InitializeComponent();
            _logger = new LoggerService(LogList, MainChart);

            _inputStreamService = new InputStreamService(_logger);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _inputStreamService.Start();
            Task.Run(PlotUpdater);
        }


        private void PlotUpdater()
        {
            while (true)
            {
                _logger.UpdatePlot(_inputStreamService.Balancer.GetSizes());
            }
        }
    }
}