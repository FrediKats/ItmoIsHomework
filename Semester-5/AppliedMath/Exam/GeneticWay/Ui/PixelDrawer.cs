using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;
using GeneticWay.Core.Vectorization;

namespace GeneticWay.Ui
{
    public class PixelDrawer
    {
        private const int FieldSize = 400;
        private const int Scale = 1;
        private const int TotalPixelSize = FieldSize * Scale;
        private readonly WriteableBitmap _writableBitmap;
        private byte[,,] _pixels;

        public PixelDrawer(Image image)
        {
            image.Height = TotalPixelSize;
            image.Width = TotalPixelSize;

            _writableBitmap = new WriteableBitmap(
                TotalPixelSize + Scale,
                TotalPixelSize + Scale,
                12,
                12,
                PixelFormats.Bgr32,
                null);

            image.Source = _writableBitmap;
        }

        public PixelDrawer AddPoints(IEnumerable<Coordinate> coordinates)
        {
            foreach (Coordinate coordinate in coordinates)
            {
                Coordinate coordinateToPrint = coordinate * Scale * FieldSize;
                PutPixel(_pixels,
                    (int)coordinateToPrint.X,
                    (int)coordinateToPrint.Y,
                    Colors.BlueViolet);
            }

            return this;
        }

        public PixelDrawer AddZones(IEnumerable<Circle> zones)
        {
            foreach (Circle zone in zones)
            {
                for (double ang = 0; ang < Math.PI * 2; ang += 0.001)
                {
                    Coordinate radiusShift = MathComputing.GetPointOnCircleCoordinate(zone.Radius, ang);
                    Coordinate pointOnField = zone.Coordinate + radiusShift;
                    Coordinate pointOnImageCoordinate = (pointOnField) * Scale * FieldSize;

                    if (pointOnField.IsOutOfPolygon() == false)
                        PutPixel(_pixels, (int)pointOnImageCoordinate.X, (int)pointOnImageCoordinate.Y, Colors.Red);
                }
            }

            return this;
        }

        public PixelDrawer AddSegments(IEnumerable<Segment> segments)
        {
            foreach (Segment segment in segments)
            {
                AddPoints(segment.ToCoordinatesList());
            }

            return this;
        }

        public PixelDrawer AddMovableObjectPoints(MovableObject movableObject)
        {
            return AddPoints(movableObject.VisitedPoints);
        }

        private static void PutPixel(byte[,,] pixels, int positionX, int positionY, Color color)
        {
            for (var addX = 0; addX < Scale; addX++)
            for (var addY = 0; addY < Scale; addY++)
            {
                if (positionX + addX < pixels.GetLength(1) && positionY + addY < pixels.GetLength(0))
                {
                    pixels[positionY + addY, positionX + addX, 0] = color.B;
                    pixels[positionY + addY, positionX + addX, 1] = color.G;
                    pixels[positionY + addY, positionX + addX, 2] = color.R;
                }
            }
        }

        public PixelDrawer PrintBackgroundWithBlack()
        {
            _pixels = new byte[TotalPixelSize + Scale, TotalPixelSize + Scale, 4];
            for (var row = 0; row < TotalPixelSize; row++)
                for (var col = 0; col < TotalPixelSize; col++)
                {
                    for (var i = 0; i < 3; i++)
                        _pixels[row, col, i] = 0;
                    _pixels[row, col, 3] = 255;
                }

            return this;
        }

        private static byte[] TransformTo1D(byte[,,] pixels)
        {
            var pixels1D = new byte[TotalPixelSize * TotalPixelSize * 4];

            var index = 0;
            for (var row = 0; row < TotalPixelSize; row++)
                for (var col = 0; col < TotalPixelSize; col++)
                    for (var i = 0; i < 4; i++)
                        pixels1D[index++] = pixels[row, col, i];

            return pixels1D;
        }

        public void PrintPixels()
        {
            byte[] pixels1D = TransformTo1D(_pixels);
            var rect = new Int32Rect(0, 0, TotalPixelSize, TotalPixelSize);
            const int stride = 4 * TotalPixelSize;

            _writableBitmap.WritePixels(rect, pixels1D, stride, 0);
        }
    }
}