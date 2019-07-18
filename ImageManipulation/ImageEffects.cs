using System;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation
{
    public static class ImageEffects
    {
        public static EditableBitmap Pixelize(CopyableBitmap inputImage)
        {
            Pixelizer pixelizer = new Pixelizer(inputImage, 5, 5);
            return pixelizer.Pixelize();
        }

        public static EditableBitmap Greyalize(CopyableBitmap inputImage)
        {
            Greyalizer greyalizer = new Greyalizer(inputImage);
            return greyalizer.Greyalize();
        }

        public static EditableBitmap Blur(CopyableBitmap inputImage)
        {
            EditableBitmap bmp = new EditableBitmap(inputImage.Width, inputImage.Height);
            for(int x = 0; x < inputImage.Width; x++)
            {
                for(int y = 0; y < inputImage.Height; y++)
                {
                    Color blurredColor = GetBlurredColor(GetNeighbors(x, y, inputImage));
                    bmp.SetPixelColor(x, y, blurredColor);
                }
            }
            return bmp;
        }

        private static List<byte[]> GetNeighbors(int x, int y, CopyableBitmap image)
        {
            List<byte[]> allColors = new List<byte[]>();
            for (int i = -5; i <= 5; i++)
            {
                for (int q = -5; q <= 5; q++)
                {
                    byte[] colors = image.GetPixelBytes(x + q, y + i);
                    if (colors != null)
                    {
                        allColors.Add(colors);
                    }
                }
            }
            return allColors;
        }

        private static Color GetBlurredColor(List<byte[]> colorData)
        {
            int[] colorSums = { 0, 0, 0, 0 };
            foreach(byte[] color in colorData)
            {
                colorSums[0] += color[0];
                colorSums[1] += color[1];
                colorSums[2] += color[2];
                colorSums[3] += color[3];
            }

            for(int i = 0; i < 4; i++)
            {
                colorSums[i] = (byte)(colorSums[i] / colorData.Count);
            }

            Color blurredColor = new Color();
            blurredColor.B = (byte)colorSums[0];
            blurredColor.G = (byte)colorSums[1];
            blurredColor.R = (byte)colorSums[2];
            blurredColor.A = (byte)colorSums[3];
            return blurredColor;
        }
    }
}
