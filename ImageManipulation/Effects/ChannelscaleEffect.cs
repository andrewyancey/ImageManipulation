namespace ImageManipulation.Effects
{
    using System.Windows.Media;
    using Imaging;

    class ChannelscaleEffect : IEffect
    {
        private CopyableBitmap _inputImage;
        private ColorChannel _channel;

        public ColorChannel Channel
        {
            get { return _channel; }
            set { _channel = value; }
        }

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
            ImageColor newColor = MapValueToChannel((byte)colorValue);
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
        /// <summary>
        /// Maps a value to a single channel in a color
        /// </summary>
        /// <param name="value">the value to apply to the channel</param>
        private ImageColor MapValueToChannel(byte value)
        {
            ImageColor newColor = new ImageColor();
            switch (_channel)
            {
                case ColorChannel.Blue:
                    newColor.B = value;
                    break;
                case ColorChannel.Green:
                    newColor.G = value;
                    break;
                case ColorChannel.Red:
                    newColor.R = value;
                    break;
                default:
                    throw new System.Exception("Channel was not set to a proper value. Channelscale does not currently accept the alpha channel");
            }
            newColor.A = 255;
            return newColor;
        }
    }
}
