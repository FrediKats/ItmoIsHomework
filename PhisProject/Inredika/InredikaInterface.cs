using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using PhysProject.Source;
using PhysProject.TestingTool;

namespace PhysProject.Inredika
{
    public partial class InredikaInterface : Window
    {
        private PhysicalField _field;

        public InredikaInterface()
        {
            InitializeComponent();
            Testing();
            ButtonStart.Click += _field.Start;
        }

        void Testing()
        {
            _field = new PhysicalField(InterfaceCanvas, 30);
            _field.AddObject(new Ticker(_field, 10, new TwoDimesional(150, 150), new TwoDimesional(0, 0)));
            Line border = Tool.GenerateLine(0, 150, 400, 150);
            border.StrokeThickness = 5;

            _field.FieldCanvas.Children.Add(border);
            ((Ticker) _field.PhysicalObjects[0]).SetDebug(TextBoxDebug);
        }
    }
}