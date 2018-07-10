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
using PhisSource.Core.Models;
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
        //TODO: Понять что это такое
        int _objectsCount = 0;

		void UpdateUserInterface()
		{
            _field = new ExecuteField(30, MainCanvas);
		    ButtonStart.Click += (sender, args) => _field.Start();

		}

		public void CreateObjectTrigger(object sender, RoutedEventArgs e)
		{
			double v = double.Parse(TextBoxV.Text);
			double a = double.Parse(TextBoxA.Text) / 180 * Math.PI;

			double vx = v * Math.Cos(a);
			double vy = v * Math.Sin(a);

			BalistModel newBall = new BalistModel(20,
				new TwoDimensional(100, int.Parse(TextBoxH.Text)), new TwoDimensional(vx, vy), a, _objectsCount);

			newBall.SetCoord(Coords);
			//newBall.IsWayDraw = true;

			_field.AddObject(newBall);
			_objectsCount++;

            BaseChart topChart = new BaseChart(gr1, "time, ticks", "y");
		    SingleAxisSeries topSeries = new SingleAxisSeries(topChart, "Y Position");
            BaseChart botChart = new BaseChart(gr2, "time, ticks", "v");
		    SingleAxisSeries velocitySeries = new SingleAxisSeries(botChart, "Velocity");

            newBall.OnPositionChanged += (o, args) => { topSeries.AddPoint(args.CurrentPosition.Y); };
            newBall.OnSpeedChanged += (o, args) => { velocitySeries.AddPoint(args.CurrentPosition.Y); };
		}
	}
}
