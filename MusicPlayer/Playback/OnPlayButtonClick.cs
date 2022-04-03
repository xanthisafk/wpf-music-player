using NAudio.Wave;
using System;
using System.Windows;

#pragma warning disable CS8622

namespace MusicPlayer
{
    public partial class MainWindow : Window
    {

        private void PlaySongs(string path)
        {
            if (!isPlaying)
            {
                string[] temp = path.Split('\\');
                string songName = temp[temp.Length - 1];
                songName = songName.Substring(0, songName.Length - 4);
                isPlaying = true;
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }
                if (audioFile == null)
                {
                    audioFile = new AudioFileReader(path);
                    outputDevice.Init(audioFile);
                    audioFile.Volume = (float)volume_ctrl.Value / 100;
                    UpdateMusicArt(path, songName);
                    UpdateSongDetails(path);
                    timer.Start();
                }
                outputDevice.Play();
                playBTN.Content = "Stop";
                playBTN.Background = System.Windows.Media.Brushes.Red;
            }
            else
            {
                StopPlaying();
            }

        }

        private void StopPlaying()
        {
            isPlaying = false;
            outputDevice?.Stop();
            RemoveSongDetails();
            playBTN.Content = "Play";
            timer.Stop();
            playBTN.Background = System.Windows.Media.Brushes.DeepSkyBlue;
        }

        private void OnPlayButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!isPlaying)
            {
                Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
                openFileDlg.Filter = "Audio files|*.wav;*.aac;*.wma;*.mp3;*.mp4;*.aiff;*.WAV;*.AAC;*.WMA;*.MP3;*.MP4;*.AIF;";
                Nullable<bool> result = openFileDlg.ShowDialog();
                if (result == true)
                {
                    PlaySongs(openFileDlg.FileName);
                }
            }
            else
            {
                PlaySongs("");
            }
        }

        private void urlTb_MouseDown(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                StopPlaying();
            }

            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.Filter = "Audio files|*.wav;*.aac;*.wma;*.mp3;*.mp4;*.aiff;*.WAV;*.AAC;*.WMA;*.MP3;*.MP4;*.AIF;";
            Nullable<bool> result = openFileDlg.ShowDialog();

            string path = "";
            if (result == true)
            {
                path = openFileDlg.FileName;
            }
            else
            {
                return;
            }
            PlaySongs(path);
        }
    }
}
