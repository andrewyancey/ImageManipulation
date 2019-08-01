using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ImageManipulation
{
    internal struct ByteArrayPoint
    {
        private int x;
        private int y;
        private byte[] bgra;

        public byte[] Bgra { get => bgra; set => bgra = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }

    internal class Convolutor
    {
        private CopyableBitmap _inputImage;
        private double[,] localKernal;

        public Convolutor(CopyableBitmap inputImage)
        {
            _inputImage = inputImage;
        }

        public EditableBitmap Convolute(double[,] kernal)
        {
            if (kernal.GetLength(0) % 2 == 0 || kernal.GetLength(1) % 2 == 0)
            {
                throw new Exception("Invalid size of kernal, expected odd size");
            }
            localKernal = kernal;
            EditableBitmap outputImage = new EditableBitmap(_inputImage.Width, _inputImage.Height);
            for (int x = 0; x < _inputImage.Width; x++)
            {
                for (int y = 0; y < _inputImage.Height; y++)
                {
                    CovolutePixel(x, y, kernal, outputImage);
                }
            }
            ((IList)localKernal).Clear();
            return outputImage;
        }

        private void CovolutePixel(int x, int y, double[,] kernal, EditableBitmap outputImage)
        {
            int xOffset = kernal.GetLength(0) / 2;
            int yOffset = kernal.GetLength(1) / 2;
            Color newColor = GetNewColor(GetNearColors(x, y, xOffset, yOffset, _inputImage));
            outputImage.SetPixelColor(x, y, newColor);
        }

        private Color GetNewColor(List<ByteArrayPoint> colorData)
        {
            int[] weightedColorSums = GetNewChannels(colorData);
            return ToColor(weightedColorSums);
        }

        private Color ToColor(int[] input)
        {
            Color newColor = new Color();
            newColor.B = (byte)input[0];
            newColor.G = (byte)input[1];
            newColor.R = (byte)input[2];
            newColor.A = (byte)input[3];
            return newColor;
        }

        private int[] GetNewChannels(List<ByteArrayPoint> colorData)
        {
            int[] colorSums = { 0, 0, 0, 0 };
            foreach (ByteArrayPoint color in colorData)
            {
                colorSums[0] += (int)(color.Bgra[0] * localKernal[color.X, color.Y]);
                colorSums[1] += (int)(color.Bgra[1] * localKernal[color.X, color.Y]);
                colorSums[2] += (int)(color.Bgra[2] * localKernal[color.X, color.Y]);
                colorSums[3] += (int)(color.Bgra[3] * localKernal[color.X, color.Y]);
            }
            return colorSums;
        }

        private List<ByteArrayPoint> GetNearColors(int x, int y, int xOffset, int yOffset, CopyableBitmap image)
        {
            List<ByteArrayPoint> capturedPixels = new List<ByteArrayPoint>();
            for (int xDifference = -xOffset; xDifference <= xOffset; xDifference++)
            {
                for (int yDifference = -yOffset; yDifference < yOffset; yDifference++)
                {
                    byte[] color = image.GetPixelBytes(x + xDifference, y + yDifference);
                    if (color != null)
                    {
                        capturedPixels.Add(new ByteArrayPoint { X = xDifference + xOffset, Y = yDifference + yOffset, Bgra = color });
                    }
                }
            }
            return capturedPixels;
        }
    }
}