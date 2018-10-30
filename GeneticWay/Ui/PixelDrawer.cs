using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GeneticWay.Core.Models;

namespace GeneticWay.Ui
{
    public class PixelDrawer
    {
        private const int FieldSize = 600;
        private const int Scale = 1;
        private const int TotalPixelSize = FieldSize * Scale;
        private readonly WriteableBitmap _writableBitmap;

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

        public void DrawPoints(IEnumerable<Coordinate> coordinates, List<Zone> zones)
        {
            var pixels = new byte[TotalPixelSize + Scale, TotalPixelSize + Scale, 4];
            PrintBackgroundWithBlack(pixels);

            foreach (Coordinate coordinate in coordinates)
            {
                var coordinateToPrint = coordinate * Scale * FieldSize;
                PutPixel(pixels, (int) (coordinateToPrint.X),
                    (int) (coordinateToPrint.Y),
                    Colors.BlueViolet);
            }

            foreach (Zone zone in zones)
            {
                for (double ang = 0; ang < Math.PI * 2; ang += 0.001)
                {
                    var newCoord = (zone.R * Math.Sin(ang), zone.R * Math.Cos(ang));
                    var circlePoint = (zone.Coordinate + newCoord) * Scale * FieldSize;
                    PutPixel(pixels, (int)circlePoint.X, (int)circlePoint.Y, Colors.Red);
                }
            }

            PrintPixels(pixels);
        }

        private static void PutPixel(byte[,,] pixels, int positionX, int positionY, Color color)
        {
            for (var addX = 0; addX < Scale; addX++)
            for (var addY = 0; addY < Scale; addY++)
            {
                pixels[positionY + addY, positionX + addX, 0] = color.B;
                pixels[positionY + addY, positionX + addX, 1] = color.G;
                pixels[positionY + addY, positionX + addX, 2] = color.R;
            }
        }

        private static void PrintBackgroundWithBlack(byte[,,] pixels)
        {
            for (var row = 0; row < TotalPixelSize; row++)
                for (var col = 0; col < TotalPixelSize; col++)
                {
                    for (var i = 0; i < 3; i++)
                        pixels[row, col, i] = 0;
                    pixels[row, col, 3] = 255;
                }
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

        private void PrintPixels(byte[,,] pixels)
        {
            var pixels1D = TransformTo1D(pixels);
            var rect = new Int32Rect(0, 0, TotalPixelSize, TotalPixelSize);
            var stride = 4 * TotalPixelSize;

            _writableBitmap.WritePixels(rect, pixels1D, stride, 0);
        }
    }
}