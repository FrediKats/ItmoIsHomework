using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GeneticWay.Models;
using GeneticWay.Tools;

namespace GeneticWay.Ui
{
    public class PixelDrawer
    {
        private const int PixelSize = 600;
        private const int Scale = 1;
        private const int Size = PixelSize * Scale;
        private readonly WriteableBitmap _writableBitmap;

        public PixelDrawer(Image image)
        {
            image.Height = Size;
            image.Width = Size;

            _writableBitmap = new WriteableBitmap(
                Size + 1,
                Size + 1,
                12,
                12,
                PixelFormats.Bgr32,
                null);

            image.Source = _writableBitmap;
        }

        public void DrawPoints(IEnumerable<Coordinate> coordinates)
        {
            var pixels = new byte[Size + 1, Size + 1, 4];
            PrintBackgroundWithBlack(pixels);

            foreach (Coordinate coordinate in coordinates)
            
 {
                for (var addX = 0; addX < Scale; addX++)
                for (var addY = 0; addY < Scale; addY++)
                    PutPixel(pixels, (int)(coordinate.X * Scale * PixelSize) + addX,
                        (int)(coordinate.Y * Scale * PixelSize) + addY,
                        Colors.BlueViolet);
            }
            PrintPixels(pixels);
        }

        private static void PutPixel(byte[,,] pixels, int positionX, int positionY, Color color)
        {
            pixels[positionY, positionX, 0] = color.B;
            pixels[positionY, positionX, 1] = color.G;
            pixels[positionY, positionX, 2] = color.R;
        }

        private static void PrintBackgroundWithBlack(byte[,,] pixels)
        {
            for (var row = 0; row < Size; row++)
                for (var col = 0; col < Size; col++)
                {
                    for (var i = 0; i < 3; i++)
                        pixels[row, col, i] = 0;
                    pixels[row, col, 3] = 255;
                }
        }

        private static byte[] TransformTo1D(byte[,,] pixels)
        {
            var pixels1D = new byte[Size * Size * 4];

            var index = 0;
            for (var row = 0; row < Size; row++)
                for (var col = 0; col < Size; col++)
                    for (var i = 0; i < 4; i++)
                        pixels1D[index++] = pixels[row, col, i];

            return pixels1D;
        }

        private void PrintPixels(byte[,,] pixels)
        {
            var pixels1D = TransformTo1D(pixels);
            var rect = new Int32Rect(0, 0, Size, Size);
            var stride = 4 * Size;

            _writableBitmap.WritePixels(rect, pixels1D, stride, 0);
        }
    }
}