﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ImageManipulation.Imaging
{
    /// <summary>
    /// wrapper for Color to support seperate data structures
    /// </summary>
    public class ImageColor
    {
        private Color _color;

        public static explicit operator byte[](ImageColor color)
        {
            byte[] channels = new byte[4];
            channels[0] = color._color.B;
            channels[1] = color._color.G;
            channels[2] = color._color.R;
            channels[3] = color._color.A;
            return channels;
        }

        public static explicit operator Color(ImageColor color)
        {
            return color._color;
        }

        public ImageColor(byte[] channels)
        {
            if (channels.Length <= 4)
            {
                _color = new Color
                {
                    B = channels[0],
                    G = channels[1],
                    R = channels[2],
                    A = channels[3]
                };
            }
            else
            {
                throw new ArgumentOutOfRangeException("an incorrect array length was passed to channels");
            }
        }

        public ImageColor(int[] channels)
        {
            if (channels.Length <= 4)
            {
                _color = new Color
                {
                    B = (byte)channels[0],
                    G = (byte)channels[1],
                    R = (byte)channels[2],
                    A = (byte)channels[3]
                };
            }
            else
            {
                throw new ArgumentOutOfRangeException("an incorrect array length was passed to channels");
            }
        }
    }
}