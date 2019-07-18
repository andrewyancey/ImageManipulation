using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation
{
    public class Blurer
    {
        private CopyableBitmap _inputImage;

        public Blurer(CopyableBitmap inputImage)
        {
            _inputImage = inputImage;
        }

        public EditableBitmap Blur()
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
        private List<byte[]> GetSurroundingColors(int x, int y, CopyableBitmap image)
        {
            List<byte[]> capturedPixels = new List<byte[]>();
            for (int verticalDifference = -5; verticalDifference <= 5; verticalDifference++)
            {
                for (int horizontalDifference = -5; horizontalDifference <= 5; horizontalDifference++)
                {
                    byte[] colors = image.GetPixelBytes(x + horizontalDifference, y + verticalDifference);
                    if (colors != null)
                    {
                        capturedPixels.Add(colors);
                    }
                }
            }
            return capturedPixels;
        }

        private Color GetBlurredColor(List<byte[]> colorData)
        {
            int[] colorSums = SumColorChannels(colorData);
            int[] blurredChannels = DivideArray(colorSums, colorData.Count);
            return ToColor(blurredChannels);
        }

        private void BlurPixel(int x, int y, EditableBitmap outputImage)
        {
            Color blurredColor = GetBlurredColor(GetSurroundingColors(x, y, _inputImage));
            outputImage.SetPixelColor(x, y, blurredColor);
        }

        // returns the sum of each individual color channel
        private int[] SumColorChannels(List<byte[]> colorData)
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

        private Color ToColor(int[] input)
        {
            Color blurredColor = new Color
            {
                B = (byte)input[0],
                G = (byte)input[1],
                R = (byte)input[2],
                A = (byte)input[3]
            };
            return blurredColor;
        }
    }
}
