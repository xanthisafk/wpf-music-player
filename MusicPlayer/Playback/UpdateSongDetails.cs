using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MusicPlayer
{
    public partial class MainWindow : Window
    {

        private string timeToString(TimeSpan t)
        {
            string m;
            string s;

            if (t.Minutes <= 9)
            {
                m = $"0{t.Minutes}";
            }
            else
            {
                m = t.Minutes.ToString();
            }

            if (t.Seconds <= 9)
            {
                s = $"0{t.Seconds}";
            }
            else
            {
                s = t.Seconds.ToString();
            }
            return $"{m}:{s}";
        }

        private void UpdateSongDetails(string path)
        {
            var tfile = TagLib.File.Create(path);
            music_name.Text = tfile.Tag.Title;
            album_name.Text = tfile.Tag.Album;
            artist_name.Text = string.Join(", ", tfile.Tag.AlbumArtists);
            genre_name.Text = string.Join(",", tfile.Tag.Genres);
            TimeSpan t = audioFile.TotalTime;
            totalTime_seek.Text = timeToString(t);
            seek_bar.IsEnabled = true;
            seek_bar.Maximum = audioFile.TotalTime.TotalSeconds;
            tfile.Dispose();
        }

        private void RemoveSongDetails()
        {
            music_name.Text = "No song playing";
            album_name.Text = string.Empty;
            artist_name.Text = string.Empty;
            genre_name.Text = string.Empty;
            currentTime_seek.Text = "--:--";
            totalTime_seek.Text = "--:--";
            seek_bar.IsEnabled = false;
            seek_bar.Value = 0;
            music_art.Source = new BitmapImage(new Uri(@"\images\listen.png", UriKind.Relative));
            playBTN.Content = "Play";
            playBTN.Background = System.Windows.Media.Brushes.DeepSkyBlue;
            playBTN.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void UpdateSeekBar(object sender, EventArgs e)
        {
            if (audioFile != null)
            {
                TimeSpan t = audioFile.CurrentTime;
                currentTime_seek.Text = timeToString(t);
                seek_bar.Value = audioFile.CurrentTime.TotalSeconds;
            }
        }

        private void DragSeekBar(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int val = (int)seek_bar.Value;
            outputDevice.Pause();
            TimeSpan t = TimeSpan.FromSeconds(val);
            audioFile.CurrentTime = t;
            outputDevice.Play();
        }
    }
}