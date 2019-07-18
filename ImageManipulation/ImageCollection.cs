using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageManipulation
{
    public class ImageCollection
    {
        List<CopyableBitmap> _images = new List<CopyableBitmap>();
        int _currentIndex = 0;

        public void AddImages(string[] paths)
        {
            foreach(string path in paths)
            {
                AddImage(path);
            }
        }

        public void AddImage(string path)
        {
            Uri uri = PathToUri(path);
            _images.Add(ImageFromPath(uri));
        }

        public BitmapImage GetCurrentImage()
        {
            return GetCurrentCopyableBitmap().Image;
        }

        public CopyableBitmap GetCurrentCopyableBitmap()
        {
            return _images[_currentIndex];
        }

        public void AdvanceIndex()
        {
            if (_currentIndex + 1 < _images.Count)
            {
                _currentIndex++;
            }
            else
            {
                _currentIndex = 0;
            }
        }

        public void ReduceIndex()
        {
            if (_currentIndex - 1 >= 0)
            {
                _currentIndex--;
            }
            else
            {
                _currentIndex = _images.Count - 1;
            }
        }

        protected CopyableBitmap ImageFromPath(Uri path)
        {
            return new CopyableBitmap(path, 800);
        }

        protected Uri PathToUri(string path)
        {
            return new Uri(path, UriKind.Relative);
        }
    }
}
