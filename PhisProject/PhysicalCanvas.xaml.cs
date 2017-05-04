using System.Windows;
using PhysProject.Source;

namespace PhysProject
{
    public partial class MainWindow : Window
    {
        private PhysicalField _field;

        public MainWindow()
        {
            InitializeComponent();
            _field = new PhysicalField(MainCanvas, Config.TimePerTick);
            _field.AddObject(new TwoDimesional(10, 10), new TwoDimesional(50, 50),
                new TwoDimesional(10, 0));
            _field.AddObject(new TwoDimesional(10, 10), new TwoDimesional(80, 80),
                new TwoDimesional(0, -10));
            ButtonStart.Click += _field.Start;
        }
    }
}
