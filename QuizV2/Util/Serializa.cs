using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
