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
using PhysProject.Core;

namespace PhysProject.MaxwellDIstribution.Views
{
	/// <summary>
	/// Логика взаимодействия для MainView.xaml
	/// </summary>
	public partial class MainView : Window
	{
		private double temperature_1 = 100, mass_1 = 16, temperature_2 = 200, mass_2 = 12;
		private const double k = 1.38, R = 8.31;
		private CustomChart _chart;
		private Func<double, double> Distribution_1, Distribution_2;
		public MainView()
		{
			InitializeComponent();
			UpdateData();
			GraphDraw();
			Draw();
		}

		void Draw()
		{
			_chart = new CustomChart(Graphic);
			_chart.AddFunctionSeries(Distribution_1, 0, 10000, 1);
			_chart.AddFunctionSeries(Distribution_2, 0, 10000, 1);
			_chart.SetAxisTitle("Velocity", "F(v)");
			//_chart.SetAxisTitle("Швиткiсть", "Probabity");
		}
		void UpdateData()
		{
			SliderTemperature1.Minimum = 0;
			SliderTemperature1.Maximum = 1000;
			SliderTemperature1.TickFrequency = 10;
			SliderTemperature1.IsSnapToTickEnabled = true;
			SliderTemperature1.ValueChanged += delegate
			{
				ValueTemperature1.Text = SliderTemperature1.Value.ToString();
				temperature_1 = SliderTemperature1.Value;
				Stat1Value1.Text = ($"{Math.Sqrt(2 * SliderTemperature1.Value * R * 1000 / SliderMass1.Value):F}");
				Stat1Value2.Text = ($"{2 * Math.Sqrt(2 * SliderTemperature1.Value * R * 1000 / (Math.PI * SliderMass1.Value)):F}");
				Stat1Value3.Text =
					($"{Math.Sqrt(3 * SliderTemperature1.Value * R * 1000 / SliderMass1.Value):F}");
				Draw();
			};
			SliderTemperature2.Minimum = 0;
			SliderTemperature2.Maximum = 1000;
			SliderTemperature2.TickFrequency = 10;
			SliderTemperature2.IsSnapToTickEnabled = true;
			SliderTemperature2.ValueChanged += delegate
			{
				ValueTemperature2.Text = SliderTemperature2.Value.ToString();
				temperature_2 = SliderTemperature2.Value;
				Stat2Value1.Text = ($"{Math.Sqrt(2 * SliderTemperature2.Value * R * 1000 / SliderMass2.Value):F}");
				Stat2Value2.Text = ($"{2 * Math.Sqrt(2 * SliderTemperature2.Value * R * 1000 / (Math.PI * SliderMass2.Value)):F}");
				Stat2Value3.Text =
					($"{Math.Sqrt(3 * SliderTemperature2.Value * R * 1000 / SliderMass2.Value):F}");
				Draw();
			};
			SliderMass1.Maximum = 400;
			SliderMass1.Minimum = 1;
			SliderMass1.TickFrequency = 1;
			SliderMass1.IsSnapToTickEnabled = true;
			SliderMass1.ValueChanged += delegate
			{
				ValueMass1.Text = SliderMass1.Value.ToString();
				mass_1 = SliderMass1.Value;
				Stat1Value1.Text = ($"{Math.Sqrt(2 * SliderTemperature1.Value * R * 1000 / SliderMass1.Value):F}");
				Stat1Value2.Text = ($"{2 * Math.Sqrt(2 * SliderTemperature1.Value * R * 1000 / (Math.PI * SliderMass1.Value)):F}");
				Stat1Value3.Text =
					($"{Math.Sqrt(3 * SliderTemperature1.Value * R * 1000 / SliderMass1.Value):F}");
				Draw();
			};
			SliderMass2.Maximum = 400;
			SliderMass2.Minimum = 1;
			SliderMass2.TickFrequency = 1;
			SliderMass2.IsSnapToTickEnabled = true;
			SliderMass2.ValueChanged += delegate
			{
				ValueMass2.Text = SliderMass2.Value.ToString();
				mass_2 = SliderMass2.Value;
				Stat2Value1.Text = ($"{Math.Sqrt(2 * SliderTemperature2.Value * R * 1000 / SliderMass2.Value):F}");
				Stat2Value2.Text = ($"{2 * Math.Sqrt(2 * SliderTemperature2.Value * R * 1000 / (Math.PI * SliderMass2.Value)):F}");
				Stat2Value3.Text =
					($"{Math.Sqrt(3 * SliderTemperature2.Value * R * 1000 / SliderMass2.Value):F}"); ;
				Draw();
			};
		}

		void GraphDraw()
		{
			Distribution_1 = delegate (double velocity)
			{
				/*return 4 * Math.PI * Math.Pow(mass_1 / (2 * Math.PI * k * temperature_1 * 1.66 * Math.Pow(10, 4)), 1.5) * velocity *
                       velocity *
                       Math.Pow(Math.E, -1 * mass_1 * velocity * velocity / (2 * k * Math.Pow(10, 4) * 1.66 * temperature_1));*/
				return 4 * Math.PI * Math.Pow(mass_1 * 1.2 * 0.0001 / (2 * Math.PI * temperature_1), 1.5) *
					   velocity * velocity *
					   Math.Pow(Math.E,
						   -1 * mass_1 * velocity * velocity * 1.2 * 0.0001 / (2 * temperature_1));
				//return 4 * Math.PI * Math.Pow(mass / (2 * Math.PI * k * 1.66 * Math.Pow(10, 4)), 1.5) * velocity * velocity *
				//       Math.Pow(Math.E, -mass * velocity * velocity / (2 * k * Math.Pow(10, 4)* 1.66 * temperature));
			};
			Distribution_2 = delegate (double velocity)
			{
				return 4 * Math.PI * Math.Pow(mass_2 * 1.2 * 0.0001 / (2 * Math.PI * temperature_2), 1.5) *
					   velocity * velocity *
					   Math.Pow(Math.E,
						   -1 * mass_2 * velocity * velocity * 1.2 * 0.0001 / (2 * temperature_2));
			};
		}
	}
}
