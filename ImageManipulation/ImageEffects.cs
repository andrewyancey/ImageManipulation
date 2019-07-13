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
            EditableBitmap myBmp = new EditableBitmap(inputImage.Width, inputImage.Height);
            int Divisions = 50;
            float exactSpacingX = (float)inputImage.Width / Divisions;
            float exactSpacingY = (float)inputImage.Height / Divisions;
            int spacingX = (int)Math.Floor(exactSpacingX);
            int spacingY = (int)Math.Floor(exactSpacingY);
            int finalSpacingX = 0;
            int finalSpacingY = 0;

            for (int xa = 0; xa < spacingX * Divisions; xa += spacingX)
            {
                for (int ya = 0; ya < spacingY * Divisions; ya += spacingY)
                {
                    Int32Rect area = new Int32Rect(xa, ya, spacingX, spacingY);
                    Color averageColor = AverageColor(inputImage, area);
                    SetColor(myBmp, area, averageColor);
                }
            }
            return myBmp;
        }

        public static EditableBitmap Greyalize(CopyableBitmap inputImage)
        {
            EditableBitmap outputImage = new EditableBitmap(inputImage.Width, inputImage.Height);
            for (int x = 0; x < inputImage.Width; x++)
            {
                for (int y = 0; y < inputImage.Height; y++)
                {
                    Color color = inputImage.GetPixel(x, y);
                    int newValue = 0;

                    newValue += color.B;
                    newValue += color.G;
                    newValue += color.R;
                    newValue += color.A;
                    newValue = newValue / 4;
                    color.B = (byte)newValue;
                    color.G = (byte)newValue;
                    color.R = (byte)newValue;
                    color.A = (byte)newValue;
                    outputImage.SetPixelColor(x, y, color);
                }
            }
            return outputImage;
        }

        private static Color AverageColor(CopyableBitmap image, Int32Rect area)
        {
            int[] channelSums = { 0, 0, 0, 0 };
            byte[,] pixels = (byte[,])image.GetPixelChannels(area).Clone();

            for (int i = 0; i < pixels.GetLength(1); i++)
            {
                channelSums[0] += pixels[0, i];
                channelSums[1] += pixels[1, i];
                channelSums[2] += pixels[2, i];
                channelSums[3] += pixels[3, i];
            }

            Color averageColor = new Color();
            averageColor.B = (byte)(channelSums[0] / pixels.GetLength(1));
            averageColor.G = (byte)(channelSums[1] / pixels.GetLength(1));
            averageColor.R = (byte)(channelSums[2] / pixels.GetLength(1));
            averageColor.A = (byte)(channelSums[3] / pixels.GetLength(1));

            return averageColor;
        }

        private static void SetColor(EditableBitmap image, Int32Rect area, Color color)
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
