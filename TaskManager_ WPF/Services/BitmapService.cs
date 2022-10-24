using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace TaskManager__WPF.Services
{
    public abstract class BitmapService
    {
        public static BitmapImage GetBitmapImageFromUrl(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.EndInit();

            return bitmap;
        }

        public static BitmapFrame ToBitMapFrame(Bitmap bitmap)
        {
            using (bitmap)
            {
                var stream = new MemoryStream();
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return BitmapFrame.Create(stream);
            }
        }

    }
}
