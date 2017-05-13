using System;
using System.Windows;
using PhysProject.Source;
using PhysProject.TestingTool;

namespace PhysProject
{
    public partial class MainWindow
    {
        void UpdateUserInterface()
        {
            _field = new PhysicalField(MainCanvas, Config.TimePerTick);
            _field.AddObject(new CustomObject(_field, 10,
                new TwoDimesional(50, 50), new TwoDimesional(10, 0)));
            _field.AddObject(new CustomObject(_field, 10,
                new TwoDimesional(80, 80), new TwoDimesional(0, -10)));

            ButtonStart.Click += _field.Start;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double v = double.Parse(TextBoxV.Text);
            double a = double.Parse(TextBoxA.Text) / 180 * Math.PI;

            double vx = v * Math.Cos(a);
            double vy = v * Math.Sin(a);
            _field.AddObject(new Balist(_field, 10,
                new TwoDimesional(100, int.Parse(TextBoxH.Text) + 5), new TwoDimesional(vx, vy)));
        }
    }
}