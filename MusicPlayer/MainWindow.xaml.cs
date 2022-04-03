using NAudio.Wave;
using System;
using System.Windows;
using System.Windows.Threading;

#pragma warning disable CS8618, CS8622

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private bool isPlaying;
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateSeekBar);
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void infoIMG_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new About().Show();
        }
    }
}
