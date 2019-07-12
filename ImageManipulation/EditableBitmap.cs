using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageManipulation
{
    public class EditableBitmap
    {
        PixelFormat _format;
        int _width;
        int _height;
        int _rawStride;
        byte[] _rawImage;
        WriteableBitmap _writableBitmap;

        public int Width
        {
            get
            {
                return _width;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
        }

        public EditableBitmap(int width, int height)
        {
            _format = PixelFormats.Bgr32;
            _width = width;
            _height = height;
            _rawStride = (_width * _format.BitsPerPixel + 7) / 8;
            _rawImage = new byte[_rawStride * _height];
            _writableBitmap = new WriteableBitmap(BitmapSource.Create(_width, _height, 96, 96, _format, null, _rawImage, _rawStride));
        }

        public void SetPixelColor(int x, int y, Color color)
        {
            VerifyCoordinates(x, y);
            Int32Rect rect = new Int32Rect(x, y, 1, 1);
            byte[] colorData = new byte[4];
            colorData[0] = color.B;
            colorData[1] = color.G;
            colorData[2] = color.R;
            colorData[3] = color.A;
            _writableBitmap.WritePixels(rect, colorData, _rawStride, 0);
        }

        public BitmapSource GetBMPSource()
        {
            return _writableBitmap;
        }

        protected bool VerifyCoordinates(int x, int y)
        {
            bool xVerified = false;
            bool yVerified = false;

            if(x >= 0 && x <= _width - 1)
            {
                xVerified = true;
            }
            else
            {
                throw new Exception("The X coordinate passed to EditableBitmap was out of bounds");
            }

            if (y >= 0 && y <= _height - 1)
            {
                yVerified = true;
            }
            else
            {
                throw new Exception("The Y coordinate passed to EditableBitmap was out of bounds");
            }

            if(xVerified && yVerified)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
