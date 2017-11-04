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
using OxyPlot;
using PhysProject.Core;
using PhysProject.Inredika.Models;

namespace PhysProject.Inredika.Views
{
	/// <summary>
	/// Логика взаимодействия для MainView.xaml
	/// </summary>
	public partial class MainView : Window
	{
		public CustomChart ChartTop, ChartBot;
		private PhysicalField _field;

		public MainView()
		{
			InitializeComponent();
			InitialCanvas();

			ButtonCreateSpring.Click += GetSpringData;
			ButtonCreateTicker.Click += GetTickerData;
			ButtonStart.Click += _field.Start;
			ButtonClean.Click += CleanCanvas;
		}

		void InitialCanvas()
		{
			_field = new PhysicalField(InterfaceCanvas, 20);
			Line border = new Line()
			{
				Name = "border",
				StrokeThickness = 2,
				Stroke = Brushes.Navy,
				X2 = 600,
			};
			_field.FieldCanvas.Children.Add(border);
			Line lineTracker = new Line()
			{
				Name = "border",
				StrokeThickness = 1,
				Stroke = Brushes.Gray,
				Y1 = 150,
				Y2 = 150,
				X2 = 600,
			};
			_field.FieldCanvas.Children.Add(lineTracker);
			Line lineTrackerSec = new Line()
			{
				Name = "border",
				StrokeThickness = 1,
				Stroke = Brushes.Gray,
				Y1 = 100,
				Y2 = 100,
				X2 = 600,
			};
			_field.FieldCanvas.Children.Add(lineTrackerSec);


			ChartTop = new CustomChart(PlotViewTop);
			ChartTop.SetAxisTitle("Время, мс", "");
			ChartBot = new CustomChart(PlotViewBot);
			ChartBot.SetAxisTitle("Время, мс", "");

			ChartBot.Model.DefaultColors[0] = OxyColors.Magenta;
			ChartBot.Model.DefaultColors[1] = OxyColors.Navy;

			ChartTop.Model.DefaultColors[0] = OxyColors.Magenta;
			ChartTop.Model.DefaultColors[1] = OxyColors.Navy;
		}

		private SpringModel _spring;
		public void GetSpringData(object sender, EventArgs e)
		{
			double mass = Double.Parse(InputSpringMass.Text);
			double coefK = Double.Parse(InputSpringCoefK.Text);
			double deltaX = Double.Parse(InputSpringDeltaY.Text);
			double coefC = Double.Parse(InputSpringCoefC.Text);

			StartSpring(mass, coefK, deltaX, coefC);
		}

		void StartSpring(double mass, double coefK, double deltaX, double coefC)
		{
			if (_spring != null)
			{
				_spring.DeleteSpring();
			}
			_spring = new SpringModel(_field, new TwoDimesional(300, 100), mass, coefK, deltaX, coefC);
			_field.AddObject(_spring);
			_spring.SeriesList.Add(new CustomSeries(ChartTop));
			_spring.SeriesList.Add(new CustomSeries(ChartBot));
			_spring.SeriesList[0]._series.Title = "Координата Y";
			_spring.SeriesList[1]._series.Title = "Скорость по Y";
		}

		private List<TickerModel> _tickerList = new List<TickerModel>();
		public void GetTickerData(object sender, EventArgs e)
		{
			double length = Double.Parse(InputTickerLength.Text);
			double angle = Double.Parse(InputTickerAngle.Text);

			StartTicker(length, angle);
		}

		void StartTicker(double length, double angle)
		{
			TickerModel _ticker = new TickerModel(_field, new TwoDimesional(150, 0), length, angle);
			_field.AddObject(_ticker);
			_tickerList.Add(_ticker);
			_ticker.SeriesList.Add(new CustomSeries(ChartTop));
			_ticker.SeriesList.Add(new CustomSeries(ChartBot));

			_ticker.SeriesList[0]._series.Title = "Координата X";
			_ticker.SeriesList[1]._series.Title = "Скорость по X";

		}

		public void CleanCanvas(object sender, EventArgs e)
		{
			foreach (TickerModel ticker in _tickerList)
			{
				ticker.DeleteTicker();
			}
			_tickerList = new List<TickerModel>();
			_spring?.DeleteSpring();
			_spring = null;
			_field.Stop(sender, e);
		}
	}
}
