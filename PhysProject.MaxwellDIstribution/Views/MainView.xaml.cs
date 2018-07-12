using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using PhysicsSource.Core.Models;

namespace PhysProject.MaxwellDIstribution.Views
{
    /// <summary>
    ///     Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private BaseChart _chart;
        private const double R = 8.31;
        private Func<double, double> _firstDistributionFunction, _secondDistributionFunction;
        private double TemperatureFirst { get; set; } = 100;
        private double MassFirst { get; set; } = 16;
        private double TemperatureSecond { get; set; } = 200;
        private double MassSecond { get; set; } = 12;

        public MainView()
        {
            InitializeComponent();
            _chart = new BaseChart(Graphic, "", "");

            InitializeFirstValue();
            InitializeSecondValue();
            GraphDraw();
            Draw();
        }

        private void Draw()
        {
            _chart = new BaseChart(Graphic, "Velocity", "F(v)");
            _chart.CleanChart();

            GenerateDistributionFunction(MassFirst, TemperatureFirst);
            GenerateDistributionFunction(MassSecond, TemperatureSecond);
            _chart.AddFunctionSeries(_firstDistributionFunction, 0, 10000, 1);
            _chart.AddFunctionSeries(_secondDistributionFunction, 0, 10000, 1);
            _chart.UpdateModel();
        }

        private void InitializeValue(Slider temperatureSlider, Slider massSlider)
        {
            temperatureSlider.Minimum = 0;
            temperatureSlider.Maximum = 1000;
            temperatureSlider.TickFrequency = 10;
            temperatureSlider.IsSnapToTickEnabled = true;

            massSlider.Minimum = 1;
            massSlider.Maximum = 400;
            massSlider.TickFrequency = 1;
            massSlider.IsSnapToTickEnabled = true;
        }

        private void UpdateUi(Slider temperatureSlider, Slider massSlider, TextBlock firstBlock, TextBlock secondBlock, TextBlock thirdBlock)
        {
            firstBlock.Text = $"{Math.Sqrt(2 * temperatureSlider.Value * R * 1000 / massSlider.Value):F}";
            secondBlock.Text =
                $"{2 * Math.Sqrt(2 * temperatureSlider.Value * R * 1000 / (Math.PI * massSlider.Value)):F}";
            thirdBlock.Text =
                $"{Math.Sqrt(3 * temperatureSlider.Value * R * 1000 / massSlider.Value):F}";
        }

        private void InitializeFirstValue()
        {
            InitializeValue(SliderTemperature1, SliderMass1);
            SliderTemperature1.ValueChanged += delegate
            {
                ValueTemperature1.Text = SliderTemperature1.Value.ToString(CultureInfo.InvariantCulture);
                TemperatureFirst = SliderTemperature1.Value;

                _firstDistributionFunction = GenerateDistributionFunction(MassFirst, TemperatureFirst);
                UpdateUi(SliderTemperature1, SliderMass1, Stat1Value1, Stat1Value2, Stat1Value3);
                Draw();
            };
            SliderMass1.ValueChanged += delegate
            {
                ValueMass1.Text = SliderMass1.Value.ToString(CultureInfo.InvariantCulture);
                MassFirst = SliderMass1.Value;

                _firstDistributionFunction = GenerateDistributionFunction(MassFirst, TemperatureFirst);
                UpdateUi(SliderTemperature1, SliderMass1, Stat1Value1, Stat1Value2, Stat1Value3);
                Draw();
            };
        }

        private void InitializeSecondValue()
        {
            InitializeValue(SliderTemperature2, SliderMass2);
            SliderTemperature2.ValueChanged += delegate
            {
                ValueTemperature2.Text = SliderTemperature2.Value.ToString(CultureInfo.InvariantCulture);
                TemperatureSecond = SliderTemperature2.Value;

                _secondDistributionFunction = GenerateDistributionFunction(MassSecond, TemperatureSecond);
                UpdateUi(SliderTemperature2, SliderMass2, Stat2Value1, Stat2Value2, Stat2Value3);
                Draw();
            };
            SliderMass2.ValueChanged += delegate
            {
                ValueMass2.Text = SliderMass2.Value.ToString(CultureInfo.InvariantCulture);
                MassSecond = SliderMass2.Value;

                _secondDistributionFunction = GenerateDistributionFunction(MassSecond, TemperatureSecond);
                UpdateUi(SliderTemperature2, SliderMass2, Stat2Value1, Stat2Value2, Stat2Value3);
                Draw();
            };
        }

        private void GraphDraw()
        {
            _firstDistributionFunction = GenerateDistributionFunction(MassFirst, TemperatureFirst);
            _secondDistributionFunction = GenerateDistributionFunction(MassSecond, TemperatureSecond);
        }

        private Func<double, double> GenerateDistributionFunction(double mass, double temperature)
        {
            return (velocity) =>
            {
                return 4 * Math.PI * Math.Pow(mass * 1.2 * 0.0001 / (2 * Math.PI * temperature), 1.5) *
                       velocity * velocity *
                       Math.Pow(Math.E,
                           -1 * mass * velocity * velocity * 1.2 * 0.0001 / (2 * temperature));
            };
        }
    }
}