using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ImageManipulation
{
    class Greyalizer
    {
        CopyableBitmap _inputImage;

        public Greyalizer(CopyableBitmap inputImage)
        {
            _inputImage = inputImage;
        }

        public EditableBitmap Greyalize()
        {
            EditableBitmap outputImage = new EditableBitmap(_inputImage.Width, _inputImage.Height);
            for (int x = 0; x < _inputImage.Width; x++)
            {
                for (int y = 0; y < _inputImage.Height; y++)
                {
                    GreyalizePixel(x, y, outputImage);
                }
            }
            return outputImage;
        }

        private void GreyalizePixel(int x, int y, EditableBitmap _outputImage)
        {
            Color originalColor = _inputImage.GetPixel(x, y);
            int colorValue = SumColorValues(originalColor);
            colorValue = colorValue / 4;
            Color newColor = ColorFromValue(colorValue);
            _outputImage.SetPixelColor(x, y, newColor);
        }

        private int SumColorValues(Color color)
        {
            int sumValue = 0;

            sumValue += color.B;
            sumValue += color.G;
            sumValue += color.R;
            sumValue += color.A;
            return sumValue;
        }

        private Color ColorFromValue(int value)
        {
            Color color = new Color();
            color.B = (byte)value;
            color.G = (byte)value;
            color.R = (byte)value;
            color.A = (byte)value;
            return color;
        }
    }
}
