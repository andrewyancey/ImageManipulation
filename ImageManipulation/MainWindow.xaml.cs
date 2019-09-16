﻿using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace ImageManipulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ImageCollection _images = new ImageCollection();

        private void image_Loaded(object sender, RoutedEventArgs e)
        {
            image.Source = _images.GetCurrentImage();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo info = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            string[] extensions = { ".jpg", ".jpeg", ".png" };
            FileInfo[] files = info.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly).Where(f => extensions.Contains(f.Extension)).ToArray();
            _images.AddImages(files.Select(f => f.FullName).ToArray());
        }

        private void NextBTN_Click(object sender, RoutedEventArgs e)
        {
            _images.AdvanceIndex();
            image.Source = _images.GetCurrentImage();
        }

        private void PreviousBTN_Click(object sender, RoutedEventArgs e)
        {
            _images.ReduceIndex();
            image.Source = _images.GetCurrentImage();
        }

        private void PixelateMenu_Click(object sender, RoutedEventArgs e)
        {
            EditableBitmap bmp = ImageEffects.Pixelize(_images.GetCurrentCopyableBitmap());
            image.Source = bmp.GetBMPSource();
        }

        private void GreyifyMenu_Click(object sender, RoutedEventArgs e)
        {
            image.Source = ImageEffects.Greyalize(_images.GetCurrentCopyableBitmap()).GetBMPSource();
        }

        private void RedscaleMenu_Click(object sender, RoutedEventArgs e)
        {
            image.Source = ImageEffects.Redscale(_images.GetCurrentCopyableBitmap()).GetBMPSource();
        }

        private void BlurMenu_Click(object sender, RoutedEventArgs e)
        {
            image.Source = ImageEffects.Blur(_images.GetCurrentCopyableBitmap()).GetBMPSource();
        }

        private void SaveFileMenu_Click(object sender, RoutedEventArgs e)
        {
            ImageSaver.SaveImage(image.Source);
        }
    }
}
