using Microsoft.UI.Xaml;
using NAudio.Wave;
using System.Windows.Forms;

namespace MusicPlayer2
{
    public sealed partial class MainWindow : Window
    {
        private void play()
        {
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
            }
            if (audioFile == null)
            {
                audioFile = new AudioFileReader(CurrentSongPath);
                outputDevice.Init(audioFile);
            }
            UpdateVolume();
            outputDevice.Play();
            Play_button.Visibility = Visibility.Collapsed;
            stop_button.Visibility = Visibility.Visible;
            UpdateMusicArt();
            UpdateSongDetails();
            timer.Start();
        }

        private void stop()
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
            outputDevice?.Stop();
            Play_button.Visibility = Visibility.Visible;
            stop_button.Visibility = Visibility.Collapsed;
            Art.Source = PlaceholderUri;
            RemoveSongDetails();
            CurrentSongPath = null;
            CurrentUri = null;
            timer.Stop();
            SeekBar_slider.Value = 0;
            SeekBar_slider.IsEnabled = false;
            CurrentTime_label.Text = "--:--";
            TotalTime_label.Text = "--:--";
        }

        private void Play_button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 Audio Files | *.mp3";
            openFileDialog.Title = "Browse";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                CurrentSongPath = openFileDialog.FileName;
            }
            else
            {
                return;
            }
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                stop();
            }
            play();
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            stop_button.Visibility = Visibility.Collapsed;
            Play_button.Visibility = Visibility.Visible;
            stop();
        }
    }
}