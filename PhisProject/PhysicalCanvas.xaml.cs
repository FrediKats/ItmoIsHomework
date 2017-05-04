using System.Windows;
using PhysProject.Source;
using PhysProject.TestingTool;

namespace PhysProject
{
    public partial class MainWindow : Window
    {
        private PhysicalField _field;

        public MainWindow()
        {
            InitializeComponent();
            _field = new PhysicalField(MainCanvas, Config.TimePerTick);
            _field.AddObject(new CustomObject(_field, new TwoDimesional(10, 10),
                new TwoDimesional(50, 50), new TwoDimesional(10, 0)));
            _field.AddObject(new CustomObject(_field, new TwoDimesional(10, 10),
                new TwoDimesional(80, 80), new TwoDimesional(10, 0)));
            _field.AddObject(new Balist(_field, new TwoDimesional(10, 10),
                new TwoDimesional(100,100), new TwoDimesional(50, 20)));
            ButtonStart.Click += _field.Start;
        }
    }
}
