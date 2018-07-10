using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PhisSource.Core.Models;

namespace PhysProject.Inredika.Models
{
	public class SpringModel : PhysicalBaseObject
	{
		private Image _springImage;

		private TwoDimensional _startPosition;
		private double _mass, _coefK, _deltaY, _coefC;
		private bool _isStarted = false;
	    private ExecuteField _field;

		public SpringModel(ExecuteField field, TwoDimensional position, double mass, double coefK, double deltaY, double coefC) : base(position, new TwoDimensional(0 , 0), 15)
        {
			CreateImage();
            _field = field;
			_startPosition = new TwoDimensional(position);
			_mass = mass;
			_coefK = coefK;
			_deltaY = deltaY;
			_coefC = coefC;
			MaterialObject.Stroke = new SolidColorBrush() { Color = Colors.Green };
		}

		protected override void CustomConduct()
		{
			if (!_isStarted)
			{
				_springImage.Height += _deltaY;
				CurrentPosition.Y += _deltaY;
				_isStarted = true;
			}

			double c = _coefC;
			double g = 9.81, m = _mass / 1000, v = SpeedVector.Y / 1000;
			double dy = CurrentPosition.Y - _startPosition.Y;

			double a = g - c * v / m - _coefK * dy / m;


			AccelerationDirection = new TwoDimensional(0, a * 100);

			UpdateData();
		}

		private void UpdateData()
		{
			//if (InfoBlock != null)
			//{
			//	InfoBlock.Text = $"Y = {Position.Y:F} м/с\n";
			//}

			//SeriesList[0]?.AddPoint(SelfTime, Position.Y);
			//SeriesList[1]?.AddPoint(SelfTime, SpeedVector.Y);
		}

		//protected override void CustomPositionUpdate()
		//{
  //          if (_springImage != null)
  //          {
  //              _springImage.Height = Position.Y;
  //          }
  //      }

		public void DeleteSpring()
		{
			_field.FieldCanvas.Children.Remove(MaterialObject);
		    _field.FieldCanvas.Children.Remove(_springImage);
		    _field.PhysicalObjects.Remove(this);
			//foreach (CustomSeries series in SeriesList)
			//{
			//	series.DeleteChart();
			//}
			//SeriesList = new List<CustomSeries>();
		}

		private void CreateImage()
		{
			if (_springImage != null)
			{
			    _field.FieldCanvas.Children.Remove(_springImage);
			}
			_springImage = new Image
			{
				Width = 20,
				Height = CurrentPosition.Y,
				Source = new BitmapImage(new Uri("SecondTicker.png", UriKind.Relative)),
				Stretch = Stretch.Fill
			};

		    //_field.FieldCanvas.Children.Add(_springImage);
			Canvas.SetLeft(_springImage, CurrentPosition.X - _springImage.Width / 2);
			Canvas.SetTop(_springImage, 0);
		}
	}
}