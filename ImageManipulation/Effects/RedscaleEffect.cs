namespace ImageManipulation.Effects
{
    using System.Windows.Media;
    using Imaging;

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
            ImageColor originalColor = new ImageColor(_inputImage.GetPixel(x, y));
            int colorValue = SumColorChannels(originalColor);
            colorValue = colorValue / 4;
            ImageColor newColor = new ImageColor();
            newColor.R = (byte)colorValue;
            newColor.A = 255;
            _outputImage.SetPixelColor(x, y, newColor);
        }

        private int SumColorChannels(ImageColor color)
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
