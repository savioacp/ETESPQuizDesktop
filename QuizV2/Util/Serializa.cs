using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows;

namespace QuizV2
{
    
    public static class Serializa
	{
        public static byte[] GetBytesFromImage(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        public static Image GetImageFromBytes(byte[] imageArray)
        {
            ImageConverter converter = new ImageConverter();
            return (Image)converter.ConvertFrom(imageArray);
        }

        public static ImageSource GetImageSourceFromImage(byte[] image)
        {
            string path = Path.GetTempFileName() + ".quiz";
            File.WriteAllBytes(path, image);
            var bmpImage = new BitmapImage();

            bmpImage.BeginInit();
            bmpImage.UriSource = new Uri(path);
            bmpImage.CacheOption = BitmapCacheOption.OnLoad;
            bmpImage.EndInit();

            return bmpImage;
        }

        public static byte[] GetImageFromImageSource(ImageSource imageSource)
        {
            try
            {
                return File.ReadAllBytes((imageSource as BitmapImage).UriSource.LocalPath);
            }
            catch(Exception e)
            {
                return null;
            }
         }
    }
}
