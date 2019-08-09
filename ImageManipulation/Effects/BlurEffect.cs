namespace ImageManipulation.Effects
{
    using System.Collections.Generic;
    using System.Windows.Media;
    using ImageManipulation.Imaging;

    public class BlurEffect : IEffect
    {
        private CopyableBitmap _inputImage;

        public EditableBitmap Apply(CopyableBitmap bitmap)
        {
            _inputImage = bitmap;
            return Blur();
        }

        private EditableBitmap Blur()
        {
            EditableBitmap outputImage = new EditableBitmap(_inputImage.Width, _inputImage.Height);
            for (int x = 0; x < _inputImage.Width; x++)
            {
                for (int y = 0; y < _inputImage.Height; y++)
                {
                    BlurPixel(x, y, outputImage);
                }
            }
            return outputImage;
        }

        // gets the color value of surrounding pixels, and returns that information as a list of byte arrays
        private List<ImageColor> GetSurroundingColors(int x, int y, CopyableBitmap image)
        {
            List<ImageColor> capturedPixels = new List<ImageColor>();
            for (int verticalDifference = -5; verticalDifference <= 5; verticalDifference++)
            {
                for (int horizontalDifference = -5; horizontalDifference <= 5; horizontalDifference++)
                {
                    byte[] channels = image.GetPixelBytes(x + horizontalDifference, y + verticalDifference);
                    if (channels != null)
                    {
                        capturedPixels.Add(new ImageColor(channels));
                    }
                }
            }
            return capturedPixels;
        }

        private ImageColor GetBlurredColor(List<ImageColor> colorData)
        {
            int[] colorSums = SumColorChannels(colorData);
            int[] blurredChannels = DivideArray(colorSums, colorData.Count);
            return new ImageColor(blurredChannels);
        }

        private void BlurPixel(int x, int y, EditableBitmap outputImage)
        {
            ImageColor blurredColor = GetBlurredColor(GetSurroundingColors(x, y, _inputImage));
            outputImage.SetPixelColor(x, y, (Color)blurredColor);
        }

        // returns the sum of each individual color channel
        private int[] SumColorChannels(List<ImageColor> colorData)
        {
            int[] colorSums = { 0, 0, 0, 0 };
            foreach (byte[] color in colorData)
            {
                colorSums[0] += color[0];
                colorSums[1] += color[1];
                colorSums[2] += color[2];
                colorSums[3] += color[3];
            }
            return colorSums;
        }

        private int[] DivideArray(int[] input, int divisor)
        {
            int[] output = (int[])input.Clone();
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (output[i] / divisor);
            }
            return output;
        }
    }
}