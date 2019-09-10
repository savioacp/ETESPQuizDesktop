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

        public static ImageSource GetImageSourceFromImage(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public static Image GetImageFromImageSource(ImageSource imageSource)
        {
            var encoder = new BmpBitmapEncoder();
            using(var ms = new MemoryStream())
            {
                encoder.Frames.Add(BitmapFrame.Create(imageSource as BitmapSource));
                encoder.Save(ms);
                ms.Position = 0;

                Bitmap bitmap = new Bitmap(ms);
                return bitmap;                
            }
        }
    }
}
