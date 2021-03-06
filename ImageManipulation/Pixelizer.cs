﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ImageManipulation
{
    class Pixelizer
    {
        int _spacingX;
        int _spacingY;
        CopyableBitmap _inputImage;

        public Pixelizer(CopyableBitmap inputImage, int spacingX, int spacingY)
        {
            _inputImage = inputImage;
            _spacingX = spacingX;
            _spacingY = spacingY;
        }

        public EditableBitmap Pixelize()
        {
            EditableBitmap myBmp = new EditableBitmap(_inputImage.Width, _inputImage.Height);

            for (int offsetX = 0; offsetX < _inputImage.Width; offsetX += _spacingX)
            {
                for (int offsetY = 0; offsetY < _inputImage.Height; offsetY += _spacingY)
                {
                    Int32Rect area = DetermineSpacingArea(offsetX, offsetY);
                    Color averageColor = AverageColor(_inputImage, area);
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

        private Color AverageColor(CopyableBitmap image, Int32Rect area)
        {
            byte[,] pixels = (byte[,])image.GetPixelChannels(area).Clone();
            int[] channelSums = SumPixelChannels(pixels);
            Color averageColor = new Color();

            averageColor.B = (byte)(channelSums[0] / pixels.GetLength(1));
            averageColor.G = (byte)(channelSums[1] / pixels.GetLength(1));
            averageColor.R = (byte)(channelSums[2] / pixels.GetLength(1));
            averageColor.A = (byte)(channelSums[3] / pixels.GetLength(1));
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

        private void SetColor(EditableBitmap image, Int32Rect area, Color color)
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
