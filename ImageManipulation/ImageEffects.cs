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
            Blurer blurer = new Blurer(inputImage);
            return blurer.Blur();
        }
    }
}
