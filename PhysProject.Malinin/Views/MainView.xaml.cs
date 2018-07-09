using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OxyPlot.Wpf;
using PhisSource.Core;
using PhysProject.Malinin.Models;

namespace PhysProject.Malinin.Views
{
	/// <summary>
	/// Логика взаимодействия для MainView.xaml
	/// </summary>
	public partial class MainView : Window
	{
		public MainView()
		{
			InitializeComponent();
			//Config.WindowHeight = (int)Height;
			UpdateUserInterface();
		}

        private ExecuteField _field;
        //private Graphic _gr1, _gr2;
        int _objectsCount = 0;

		void UpdateUserInterface()
		{
            _field = new ExecuteField(30, MainCanvas);
            SetGraphics();
		    ButtonStart.Click += (sender, args) => _field.Start();

		}

		void SetGraphics()
		{
			//_gr1 = new Graphic(gr1);
			//_gr2 = new Graphic(gr2);
			LineSeries series_At = new LineSeries(), series_An = new LineSeries();

			//_gr1.Model.Series.Add(series_At);
			//_gr1.SetAxisTitle("Время, мс", "At, м/с2");

			//_gr2.Model.Series.Add(series_An);
			//_gr2.SetAxisTitle("Время, мс", "An, м/с2");
		}

		public void CreateObjectTrigger(object sender, RoutedEventArgs e)
		{
			double v = double.Parse(TextBoxV.Text);
			double a = double.Parse(TextBoxA.Text) / 180 * Math.PI;

			double vx = v * Math.Cos(a);
			double vy = v * Math.Sin(a);

			BalistModel newBall = new BalistModel(20,
				new TwoDimensional(100, int.Parse(TextBoxH.Text) + 2.5), new TwoDimensional(vx, vy), a, _objectsCount);

			//newBall.AddGraphic(_gr1);
			//newBall.AddGraphic(_gr2);
			newBall.SetCoord(Coords);
			newBall.IsWayDraw = true;

			_field.AddObject(newBall);
			_objectsCount++;
		}
	}
}
