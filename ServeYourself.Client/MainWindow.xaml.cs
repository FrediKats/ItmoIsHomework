using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ServeYourself.Client.UserControllers;
using ServeYourself.Core.ServeServices;

namespace ServeYourself.Client
{
    public partial class MainWindow : Window
    {
        private readonly IServeService _simpleServe;
        private readonly List<VisitableStatisticController> _controlList;
        public MainWindow()
        {
            InitializeComponent();
            _simpleServe = new ChainServeService();
            _controlList = _simpleServe
                .GetAllVisitableList()
                .Select(v => new VisitableStatisticController(v) {Height = 200})
                .ToList();

            foreach (VisitableStatisticController control in _controlList)
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
            _simpleServe.Iteration();
            _controlList.ForEach(c => c.UpdateControl());
        }
    }
}
