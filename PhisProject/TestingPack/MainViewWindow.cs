using System.Windows;
using System.Windows.Shapes;
using OxyPlot.Series;
using PhysProject.Source;

namespace PhysProject.TestingPack
{
    public partial class MainViewWindow : Window
    {
        private PhysicalField _field;
        private Graphic _gr;

        private int _maxHeignt = 250;

        public MainViewWindow()
        {
            InitializeComponent();
            Testing();
            ButtonStart.Click += _field.Start;
            _gr = new Graphic(TestingGraph);
            

            LineSeries series = new LineSeries();
            _gr.Model.Series.Add(series);

            _field.PhysicalObjects[0].AddGraphic(_gr);
            _gr.SetAxisTitle("Time, ms", "X");
        }

        void Testing()
        {
            _field = new PhysicalField(InterfaceCanvas, 30);
            _field.AddObject(new Ticker(_field, 10, new TwoDimesional(200, _maxHeignt), new TwoDimesional(0, 0)));
            Line border = Tool.GenerateLine(0, _maxHeignt, 400, _maxHeignt);
            border.StrokeThickness = 5;

            _field.FieldCanvas.Children.Add(border);
            ((Ticker) _field.PhysicalObjects[0]).SetDebug(TextBoxDebug);
        }
    }
}