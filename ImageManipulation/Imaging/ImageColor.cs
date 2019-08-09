using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ImageManipulation.Imaging
{
    /// <summary>
    /// Wrapper class for Color to support seperate data structures
    /// </summary>
    public class ImageColor
    {
        private Color _color;

        public ImageColor() { _color = new Color(); }

        public ImageColor(byte[] channels)
        {
            if (channels.Length != 4) throw new ArgumentOutOfRangeException("An incorrect array length was passed to channels");

            _color = new Color
            {
                B = channels[0],
                G = channels[1],
                R = channels[2],
                A = channels[3]
            };
        }

        public ImageColor(int[] channels)
        {
            if (channels.Any(c => c > byte.MaxValue)) throw new Exception("The passed array had values greater than byte");

            if (channels.Length != 4) throw new ArgumentOutOfRangeException("An incorrect array length was passed to channels");

            _color = new Color
            {
                B = (byte)channels[0],
                G = (byte)channels[1],
                R = (byte)channels[2],
                A = (byte)channels[3]
            };
        }

        public ImageColor(Color color)
        {
            _color = color;
        }

        /// <summary>
        /// Blue channel
        /// </summary>
        public byte B
        {
            get { return _color.B; }
            set { _color.B = value; }
        }
        /// <summary>
        /// Green channel
        /// </summary>
        public byte G
        {
            get { return _color.G; }
            set { _color.G = value; }
        }
        /// <summary>
        /// Red channel
        /// </summary>
        public byte R
        {
            get { return _color.R; }
            set { _color.R = value; }
        }
        /// <summary>
        /// Alpha channel
        /// </summary>
        public byte A
        {
            get { return _color.A; }
            set { _color.A = value; }
        }

        public static explicit operator byte[](ImageColor color)
        {
            byte[] channels = new byte[4];
            channels[0] = color._color.B;
            channels[1] = color._color.G;
            channels[2] = color._color.R;
            channels[3] = color._color.A;
            return channels;
        }

        public static implicit operator Color(ImageColor color)
        {
            return color._color;
        }
        /// <summary>
        /// Takes a single value and applies it to all channels
        /// </summary>
        /// <param name="value">The value to apply</param>
        /// <returns>ImageColor from value</returns>
        public static ImageColor ColorFromValue(int value)
        {
            if (value > byte.MaxValue) throw new ArgumentOutOfRangeException("the value passed had values greater than byte");
            ImageColor color = new ImageColor
            {
                B = (byte)value,
                G = (byte)value,
                R = (byte)value,
                A = (byte)value
            };
            return color;
        }
    }
}
