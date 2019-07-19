using System.Windows.Media;

namespace ImageManipulation.Effects
{
    class RedscaleEffect : IEffect
    {
        private CopyableBitmap _inputImage;

        public EditableBitmap Apply(CopyableBitmap bitmap)
        {
            _inputImage = bitmap;
            return RedscaleImage();
        }

        private EditableBitmap RedscaleImage()
        {
            EditableBitmap outputImage = new EditableBitmap(_inputImage.Width, _inputImage.Height);
            for (int x = 0; x < _inputImage.Width; x++)
            {
                for (int y = 0; y < _inputImage.Height; y++)
                {
                    RedscalePixel(x, y, outputImage);
                }
            }

            return outputImage;
        }

        private void RedscalePixel(int x, int y, EditableBitmap _outputImage)
        {
            Color originalColor = _inputImage.GetPixel(x, y);
            int colorValue = SumColorValues(originalColor);
            colorValue = colorValue / 4;
            Color newColor = RedChannelToValue(colorValue);
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

        private Color RedChannelToValue(int value)
        {
            Color color = new Color
            {
                B = 0,
                G = 0,
                R = (byte)value,
                A = 255
            };
            return color;
        }
    }
}
