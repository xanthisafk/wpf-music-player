using System.Windows;

namespace MusicPlayer
{
    public partial class MainWindow : Window
    {
        private void volume_ctrl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (audioFile != null)
            {
                audioFile.Volume = (float)e.NewValue / 100;
            }
            if (volume_lbl != null)
            {
                volume_lbl.Text = "Volume: " + (int)e.NewValue;
            }
        }
    }
}
