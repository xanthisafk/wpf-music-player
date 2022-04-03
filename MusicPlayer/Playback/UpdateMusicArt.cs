using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MusicPlayer
{
    public partial class MainWindow : Window
    {
        private void UpdateMusicArt(string path, string location)
        {
            var file = TagLib.File.Create(path);
            if (file.Tag.Pictures.Length >= 1)
            {
                var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                Image img = Image.FromStream(new MemoryStream(bin)).GetThumbnailImage(1000, 1000, null, IntPtr.Zero);
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Jpeg);
                    ms.Position = 0;

                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = ms;
                    bi.EndInit();
                    music_art.Source = bi;
                    UpdateBackgroundColor(img);
                }

                img.Dispose();

            }
        }

        public void UpdateBackgroundColor(Image img)
        {
            Bitmap theBitMap = (Bitmap)img;

            var dctColorIncidence = new Dictionary<int, int>();

            // this is what you want to speed up with unmanaged code
            for (int row = 0; row < theBitMap.Size.Width / 2; row++)
            {
                for (int col = 0; col < theBitMap.Size.Height / 2; col++)
                {
                    var pixelColor = theBitMap.GetPixel(row, col).ToArgb();

                    if (dctColorIncidence.Keys.Contains(pixelColor))
                    {
                        dctColorIncidence[pixelColor]++;
                    }
                    else
                    {
                        dctColorIncidence.Add(pixelColor, 1);
                    }
                }
            }

            // note that there are those who argue that a
            // .NET Generic Dictionary is never guaranteed
            // to be sorted by methods like this
            var dctSortedByValueHighToLow = dctColorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            var lc = Color.FromArgb(dctSortedByValueHighToLow.First().Key);
            var sysCol = System.Windows.Media.Color.FromArgb(lc.A, lc.R, lc.G, lc.B);
            var brush = new System.Windows.Media.SolidColorBrush(sysCol);
            mainWindow.Background = brush;
            urlTb.Foreground = brush;
            var argb = lc.ToArgb() ^ 0xffffff;
            var nlc = Color.FromArgb(argb);
            var inverted = System.Windows.Media.Color.FromArgb(nlc.A, nlc.R, nlc.G, nlc.B);
            brush = new System.Windows.Media.SolidColorBrush(inverted);
            mainWindow.Foreground = brush;
            urlTb.Background = brush;

        }

    }
}

