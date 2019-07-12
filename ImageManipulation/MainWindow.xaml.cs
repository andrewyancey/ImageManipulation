using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageManipulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CopyableBitmap[] _images = new CopyableBitmap[2];
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
            if(_currentIndex + 1 < _images.Length)
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
            _images[0] = LoadImage(PathToUri("dolphinfront.jpg"));
            _images[1] = LoadImage(PathToUri("photo.jpg"));
            AssignImageFromIndex();
        }

        private void NextBTN_Click(object sender, RoutedEventArgs e)
        {
            AdvanceIndex();
            AssignImageFromIndex();
        }

        private void PixelateBTN_Click(object sender, RoutedEventArgs e)
        {
            EditableBitmap bmp = Pixelizer.Pixelize(_images[_currentIndex]);
            image.Source = bmp.GetBMPSource();
        }
    }
}
