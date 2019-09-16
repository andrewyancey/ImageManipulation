using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageManipulation
{
    class ImageSaver
    {
        public static void SaveImage(ImageSource source)
        {
            using (var dialog = new System.Windows.Forms.SaveFileDialog())
            {
                dialog.Filter = @"PNG files|*.png";
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    using (var fileStream = new FileStream(dialog.FileName, FileMode.Create))
                    {
                        try
                        {
                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)source));
                            encoder.Save(fileStream);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
