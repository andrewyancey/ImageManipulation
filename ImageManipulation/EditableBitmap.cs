using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageManipulation
{
    public class EditableBitmap
    {
        private PixelFormat _format;
        private int _rawStride;
        private byte[] _rawImage;
        private WriteableBitmap _writableBitmap;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public EditableBitmap(int width, int height)
        {
            _format = PixelFormats.Bgr32;
            Width = width;
            Height = height;
            _rawStride = (Width * _format.BitsPerPixel + 7) / 8;
            _rawImage = new byte[_rawStride * Height];
            _writableBitmap = new WriteableBitmap(BitmapSource.Create(Width, Height, 96, 96, _format, null, _rawImage, _rawStride));
        }

        public void SetPixelColor(int x, int y, Color color)
        {
            VerifyCoordinates(x, y);
            Int32Rect rect = new Int32Rect(x, y, 1, 1);
            byte[] colorData = new byte[4] { color.B, color.G, color.R, color.A };
            _writableBitmap.WritePixels(rect, colorData, _rawStride, 0);
        }

        public BitmapSource GetBMPSource()
        {
            return _writableBitmap;
        }

        protected bool VerifyCoordinates(int x, int y)
        {
            bool xVerified = x >= 0 && x <= Width - 1;
            bool yVerified = y >= 0 && y <= Height - 1;

            if (!xVerified)
            {
                throw new Exception("The X coordinate passed to EditableBitmap was out of bounds");
            }
            else if (!yVerified)
            {
                throw new Exception("The Y coordinate passed to EditableBitmap was out of bounds");
            }

            return xVerified && yVerified;
        }
    }
}
