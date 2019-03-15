using System.Windows;
using ServeYourself.Client.UserControllers;
using ServeYourself.Core;

namespace ServeYourself.Client
{
    public partial class MainWindow : Window
    {
        private readonly ServeService _serve;
        private VisitableStatisticController _controller;
        public MainWindow()
        {
            InitializeComponent();
            _serve = new ServeService();
            _controller = new VisitableStatisticController(_serve.Shop) {Height = 400};
            ElementsList.Children.Add(_controller);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IterationCount.Text, out int value))
            {
                for (int i = 0; i < value; i++)
                {
                    _serve.Iteration();
                    _controller.UpdateControl();
                }
            }
            else
            {
                _serve.Iteration();
                _controller.UpdateControl();
            }
        }
    }
}
