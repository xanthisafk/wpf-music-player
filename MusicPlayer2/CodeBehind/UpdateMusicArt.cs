using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using System.IO;

namespace MusicPlayer2
{
    public partial class MainWindow : Window
    {
        private void UpdateMusicArt()
        {
            var file = TagLib.File.Create(CurrentSongPath);
            if (!(file.Tag.Pictures.Length >= 1))
            { return; }

            byte[] b = file.Tag.Pictures[0].Data.Data;
            using (FileStream fs = new FileStream("art", FileMode.Create))
            {
                fs.Write(b);
                CurrentUri = fs.Name;
                Art.Source = new BitmapImage(new System.Uri(fs.Name));
            }
            file.Dispose();
        }
    }
}