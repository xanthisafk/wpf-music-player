using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using NAudio.Wave;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MusicPlayer2
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private string CurrentUri;
        private ImageSource PlaceholderUri;
        private string CurrentSongPath;
        DispatcherTimer timer;

        public MainWindow()
        {
            this.InitializeComponent();
            mainWindow.Title = "Music player";
            PlaceholderUri = Art.Source;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler<object>(UpdateSeekBar);
            timer.Interval = new TimeSpan(0, 0, 1);

            var uiSettings = new Windows.UI.ViewManagement.UISettings();
            OuterColor.Color = uiSettings.GetColorValue(Windows.UI.ViewManagement.UIColorType.Accent);

            Blur_ellipse.Opacity = 0.1;

            //Update window size
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32 { Width = 500, Height = 800 });

        }
    }
}
