namespace ImageManipulation.Effects
{
    using System.Windows.Media;
    using Imaging;

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
            ImageColor originalColor = new ImageColor(_inputImage.GetPixel(x, y));
            int colorValue = SumColorValues(originalColor);
            colorValue = colorValue / 4;
            ImageColor newColor = ImageColor.ColorFromValue(colorValue);
            _outputImage.SetPixelColor(x, y, newColor);
        }

        private int SumColorValues(ImageColor color)
        {
            int sumValue = 0;

            sumValue += color.B;
            sumValue += color.G;
            sumValue += color.R;
            sumValue += color.A;

            return sumValue;
        }
    }
}