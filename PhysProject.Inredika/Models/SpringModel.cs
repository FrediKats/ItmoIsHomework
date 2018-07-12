using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PhysicsSource.Core.Models;

namespace PhysProject.Inredika.Models
{
    public class SpringModel : PhysicalBaseObject
    {
        //TODO: refactoring
        private readonly ExecuteField _field;
        private bool _isStarted;
        private readonly double _mass;
        private readonly double _coefK;
        private readonly double _deltaY;
        private readonly double _coefC;
        private Image _springImage;

        private readonly TwoDimensional _startPosition;

        public SpringModel(ExecuteField field, TwoDimensional position, double mass, double coefK, double deltaY,
            double coefC) : base(position, new TwoDimensional(0, 0), 15)
        {
            //CreateImage();
            _field = field;
            _startPosition = new TwoDimensional(position);
            _mass = mass;
            _coefK = coefK;
            _deltaY = deltaY;
            _coefC = coefC;
            MaterialObject.Stroke = new SolidColorBrush {Color = Colors.Green};
        }

        public void DeleteSpring()
        {
            _field.FieldCanvas.Children.Remove(MaterialObject);
            _field.FieldCanvas.Children.Remove(_springImage);
            _field.PhysicalObjects.Remove(this);
        }

        protected override void CustomConduct()
        {
            if (!_isStarted)
            {
                _springImage.Height += _deltaY;
                CurrentPosition.Y += _deltaY;
                _isStarted = true;
            }

            //TODO: refactoring
            double g = 9.81;
            double m = _mass / 1000;
            double v = SpeedVector.Y / 1000;
            var dy = CurrentPosition.Y - _startPosition.Y;

            var a = g - _coefC * v / m - _coefK * dy / m;


            AccelerationDirection = new TwoDimensional(0, a * 100);
        }

        //private void CreateImage()
        //{
        //    //TODO: refactoring
        //    if (_springImage != null) _field.FieldCanvas.Children.Remove(_springImage);
        //    _springImage = new Image
        //    {
        //        Width = 20,
        //        Height = CurrentPosition.Y,
        //        Source = new BitmapImage(new Uri("SecondTicker.png", UriKind.Relative)),
        //        Stretch = Stretch.Fill
        //    };

        //    Canvas.SetLeft(_springImage, CurrentPosition.X - _springImage.Width / 2);
        //    Canvas.SetTop(_springImage, 0);
        //}
    }
}