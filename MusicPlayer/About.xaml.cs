using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = @"https://www.flaticon.com/packs?type=icon&shape=lineal-color&order_by=4",
                UseShellExecute = true
            });
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.github.com/xanthisafk/wpf-music-player",
                UseShellExecute = true
            });
        }
    }
}
