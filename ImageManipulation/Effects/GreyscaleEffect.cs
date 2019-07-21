﻿namespace ImageManipulation.Effects
{
    using System.Windows.Media;

    public class GreyscaleEffect : IEffect
    {
        private CopyableBitmap _inputImage;

        public EditableBitmap Apply(CopyableBitmap bitmap)
        {
            _inputImage = bitmap;
            return Greyalize();
        }

        private EditableBitmap Greyalize()
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
            Color color = new Color
            {
                B = (byte)value,
                G = (byte)value,
                R = (byte)value,
                A = (byte)value
            };
            return color;
        }
    }
}