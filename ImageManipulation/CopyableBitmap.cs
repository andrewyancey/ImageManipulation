using System;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageManipulation
{
    public class CopyableBitmap
    {
        private int _stride;
        private byte[] _rawPixels;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public BitmapImage Image { get; private set; }


        public CopyableBitmap(Uri path, int width)
        {
            
            Image = new BitmapImage();
            Image.BeginInit();
            Image.DecodePixelWidth = width;
            Image.UriSource = path;
            Image.EndInit();

            Width = Image.PixelWidth;
            Height = Image.PixelHeight;
            _stride = (Image.PixelWidth * Image.Format.BitsPerPixel + 7) / 8;
            _rawPixels = new byte[_stride * Image.PixelHeight];
            Image.CopyPixels(_rawPixels, _stride, 0);
        }

        public byte[,] GetPixelChannels(Int32Rect area)
        {
            byte[,] returnArrays = new byte[4, area.Width * area.Height];
            int index = 0;
            for (int x = area.X; x < area.X + area.Width; x++)
            {
                for (int y = area.Y; y < area.Y + area.Height; y++)
                {
                    int pixelIndex = CoordinateToIndex(x, y);
                    returnArrays[0, index] = _rawPixels[pixelIndex];
                    returnArrays[1, index] = _rawPixels[pixelIndex + 1];
                    returnArrays[2, index] = _rawPixels[pixelIndex + 2];
                    returnArrays[3, index] = _rawPixels[pixelIndex + 3];
                    index++;
                }
            }
            return returnArrays;
        }

        public Color GetPixel(int x, int y)
        {
            int index = CoordinateToIndex(x, y);
            Color returnColor = new Color
            {
                B = _rawPixels[index],
                G = _rawPixels[index + 1],
                R = _rawPixels[index + 2],
                A = _rawPixels[index + 3]
            };
            return returnColor;
        }

        public byte[] GetPixelBytes(int x, int y)
        {
            int index = CoordinateToIndex(x, y);
            if (index >= 0 && index <= _rawPixels.Length - 5)
            {
                byte[] pixelBytes = new byte[4];
                pixelBytes[0] = _rawPixels[index];
                pixelBytes[1] = _rawPixels[index + 1];
                pixelBytes[2] = _rawPixels[index + 2];
                pixelBytes[3] = _rawPixels[index + 3];
                return pixelBytes;
            }
            else
            {
                return null;
            }
        }

        protected int CoordinateToIndex(int x, int y)
        {
            // TODO: 4 is the number of bytes per pixel. This needs to be changed so that it reflects the format
            if (x >= 0 && y >= 0)
            {
                if (x < Image.Width && y < Image.Height)
                {
                    return (y * Image.PixelWidth * 4) + (x * 4);
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                return -1;
            }
        }
    }
}
