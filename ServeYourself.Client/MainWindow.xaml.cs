using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ServeYourself.Client.UserControllers;
using ServeYourself.Core;

namespace ServeYourself.Client
{
    public partial class MainWindow : Window
    {
        private readonly ServeService _serve;
        private readonly List<VisitableStatisticController> _controlList;
        public MainWindow()
        {
            InitializeComponent();
            _serve = new ServeService();
            _controlList = _serve
                .GetAllVisitableList()
                .Select(v => new VisitableStatisticController(v) {Height = 250})
                .ToList();

            foreach (var control in _controlList)
            {
                ElementsList.Children.Add(control);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IterationCount.Text, out int value))
            {
                for (int i = 0; i < value; i++)
                {
                    UpdateControls();
                }
            }
            else
            {
                UpdateControls();
            }
        }

        private void UpdateControls()
        {
            _serve.Iteration();
            _controlList.ForEach(c => c.UpdateControl());
        }
    }
}
