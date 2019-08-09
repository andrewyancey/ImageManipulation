namespace ImageManipulation.Effects
{
    using System.Windows;
    using System.Windows.Media;
    using ImageManipulation.Imaging;
    using System.Linq;

    public class PixelizeEffect : IEffect
    {
        private readonly int _spacingX;

        private readonly int _spacingY;

        private CopyableBitmap _inputImage;

        public PixelizeEffect(int spacingX, int spacingY)
        {
            _spacingX = spacingX;
            _spacingY = spacingY;
        }

        public EditableBitmap Apply(CopyableBitmap bitmap)
        {
            _inputImage = bitmap;
            return Pixelize();
        }

        private EditableBitmap Pixelize()
        {
            EditableBitmap myBmp = new EditableBitmap(_inputImage.Width, _inputImage.Height);

            for (int offsetX = 0; offsetX < _inputImage.Width; offsetX += _spacingX)
            {
                for (int offsetY = 0; offsetY < _inputImage.Height; offsetY += _spacingY)
                {
                    Int32Rect area = DetermineSpacingArea(offsetX, offsetY);
                    ImageColor averageColor = AverageColor(_inputImage, area);
                    SetColor(myBmp, area, averageColor);
                }
            }
            return myBmp;
        }

        private Int32Rect DetermineSpacingArea(int offsetX, int offsetY)
        {
            // decide if there is enough room for the entire spacing and 
            // if not then just use the remaining pixels
            int width = 0;
            int height = 0;
            if (offsetX + _spacingX < _inputImage.Width)
            {
                width = _spacingX;
            }
            else
            {
                width = _inputImage.Width - offsetX;
            }
            if (offsetY + _spacingY < _inputImage.Height)
            {
                height = _spacingY;
            }
            else
            {
                height = _inputImage.Height - offsetY;
            }
            return new Int32Rect(offsetX, offsetY, width, height);
        }

        private ImageColor AverageColor(CopyableBitmap image, Int32Rect area)
        {
            byte[,] pixels = (byte[,])image.GetPixelChannels(area).Clone();
            int[] channelSums = SumPixelChannels(pixels);
            ImageColor averageColor = new ImageColor(channelSums.Select(t => t / pixels.GetLength(1)).ToArray());
            return averageColor;
        }

        private int[] SumPixelChannels(byte[,] pixels)
        {
            int[] channelSums = { 0, 0, 0, 0 };

            for (int i = 0; i < pixels.GetLength(1); i++)
            {
                channelSums[0] += pixels[0, i];
                channelSums[1] += pixels[1, i];
                channelSums[2] += pixels[2, i];
                channelSums[3] += pixels[3, i];
            }

            return channelSums;
        }

        private void SetColor(EditableBitmap image, Int32Rect area, ImageColor color)
        {
            for (int x = area.X; x < area.X + area.Width; x++)
            {
                for (int y = area.Y; y < area.Y + area.Height; y++)
                {
                    image.SetPixelColor(x, y, color);
                }
            }
        }
    }
}