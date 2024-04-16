using System.Windows;
using System.Windows.Controls;
using SpeedTestSharp.Client;
using SpeedTestSharp.Enums;

namespace NetworkTool.Pages
{
    /// <summary>
    /// Логика взаимодействия для SpeedTestPage.xaml
    /// </summary>
    public partial class SpeedTestPage : Page
    {
        public SpeedTestPage()
        {
            InitializeComponent();
        }

/*        private async void StartSpeedTestButton_Click(object sender, RoutedEventArgs e)
        {
            var speedtestClient = new SpeedTestClient();
            var result = await speedtestClient.TestSpeedAsync(SpeedUnit.MBps);

            DownloadProgress.Value = 100;
            UploadProgress.Value = 100;

            DownloadSpeed.Text = $"{result.DownloadSpeed} Мбит/с";
            UploadSpeed.Text = $"{result.UploadSpeed} Мбит/с";
        }*/

        private async void RunButton_OnClick(object sender, RoutedEventArgs e)
        {
            var stClient = new SpeedTestClient();
            stClient.ProgressChanged += (o, info) =>
            {
                Dispatcher.Invoke(() => Progress.Value += 1);
                
            };
            var result = await stClient.TestSpeedAsync(SpeedUnit.Mbps);
            DownloadSpeedTextBlock.Text = $"{result.DownloadSpeed} {result.SpeedUnit}";
            UploadSpeedTextBlock.Text = $"{result.UploadSpeed} {result.SpeedUnit}";
        }
    }
}
