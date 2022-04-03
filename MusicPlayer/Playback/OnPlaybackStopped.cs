using NAudio.Wave;
using System.Windows;

#pragma warning disable CS8625

namespace MusicPlayer
{
    public partial class MainWindow : Window
    {
        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (outputDevice != null)
            {
                outputDevice.Dispose();
                outputDevice = null;
            }
            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
            RemoveSongDetails();
            timer.Stop();
            playBTN.Background = System.Windows.Media.Brushes.DeepSkyBlue;
            playBTN.Content = "Play";
        }
    }
}
