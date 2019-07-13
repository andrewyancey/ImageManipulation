using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;


namespace ImageManipulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CopyableBitmap> _images = new List<CopyableBitmap>();
        int _currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected CopyableBitmap LoadImage(Uri path)
        {
            return new CopyableBitmap(path, 800);
        }

        protected Uri PathToUri(string path)
        {
            return new Uri(path, UriKind.Relative);
        }

        protected void AdvanceIndex()
        {
            if(_currentIndex + 1 < _images.Count)
            {
                _currentIndex++;
            }
            else
            {
                _currentIndex = 0;
            }
        }

        protected void AssignImageFromIndex()
        {
            image.Source = _images[_currentIndex].Image;
        }

        private void image_Loaded(object sender, RoutedEventArgs e)
        {

            AssignImageFromIndex();
        }

        private void NextBTN_Click(object sender, RoutedEventArgs e)
        {
            AdvanceIndex();
            AssignImageFromIndex();
        }

        private void PixelateBTN_Click(object sender, RoutedEventArgs e)
        {
            EditableBitmap bmp = ImageEffects.Pixelize(_images[_currentIndex]);
            image.Source = bmp.GetBMPSource();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo info = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            string[] extensions = { ".jpg", ".jpeg", ".png" };
            FileInfo[] files = info.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly).Where(f => extensions.Contains(f.Extension)).ToArray();

            foreach(FileInfo file in files)
            {
                Uri path = PathToUri(file.FullName);
                _images.Add(LoadImage(path));
            }
        }

        private void GreyifyBTN_Click(object sender, RoutedEventArgs e)
        {
            image.Source = ImageEffects.Greyalize(_images[_currentIndex]).GetBMPSource();
        }
    }
}
