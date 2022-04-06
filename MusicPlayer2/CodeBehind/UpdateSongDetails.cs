using Microsoft.UI.Xaml;

namespace MusicPlayer2
{
    public partial class MainWindow : Window
    {

        private string timeToString(System.TimeSpan t)
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

        private void UpdateSongDetails()
        {
            TagLib.File file = TagLib.File.Create(CurrentSongPath);
            string artists = string.Join(", ", file.Tag.Performers);
            song_title.Text = $"{artists} - {file.Tag.Title}";
            SeekBar_slider.Maximum = (int)audioFile.TotalTime.TotalSeconds;
            SeekBar_slider.IsEnabled = true;
            TotalTime_label.Text = timeToString(audioFile.TotalTime);
            CurrentTime_label.Text = timeToString(audioFile.CurrentTime);
        }

        private void RemoveSongDetails()
        {
            song_title.Text = "No song playing";
        }

        /// <Summary>
        /// Called every second by `timer` to update seekbar and CurrentTime_label
        /// </Summary>
        private void UpdateSeekBar(object sender, object e)
        {
            SeekBar_slider.Value = (int)audioFile.CurrentTime.TotalSeconds;
            CurrentTime_label.Text = timeToString(audioFile.CurrentTime);
        }


        /// <Summary>
        /// Seeks song to updated time and updates CurrentTime_label
        /// </Summary>
        private void SeekBar_slider_DragLeave(object sender, RoutedEventArgs e)
        {
            outputDevice.Pause();
            System.TimeSpan t = System.TimeSpan.FromSeconds(SeekBar_slider.Value);
            audioFile.CurrentTime = t;
            CurrentTime_label.Text = timeToString(t);
            outputDevice.Play();
            timer.Start();
        }

        /// <Summary>
        /// Updates CurrentTime_label while slider is being manipulated
        /// </Summary>
        private void SeekBar_slider_ManipulationDelta(object sender, Microsoft.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs e)
        {
            System.TimeSpan t = System.TimeSpan.FromSeconds(SeekBar_slider.Value);
            CurrentTime_label.Text = timeToString(t);
        }

        /// <Summary>
        /// Stops timer when seekbar is being manipulated
        /// </Summary>
        private void SeekBar_slider_ManipulationStarted(object sender, Microsoft.UI.Xaml.Input.ManipulationStartedRoutedEventArgs e)
        {
            timer.Stop();
        }

        /// <Summary>
        /// Updates volume when volume slider is updated
        /// </Summary>
        private void Volume_slider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (Volume_slider == null || audioFile == null)
            {
                return;
            }

            UpdateVolume();
        }

        private void UpdateVolume()
        {
            float val = (float)Volume_slider.Value / 100;
            audioFile.Volume = val;
        }
    }
}