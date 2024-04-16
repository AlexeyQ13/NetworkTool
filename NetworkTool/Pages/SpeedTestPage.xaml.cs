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

        private async void StartSpeedTestButton_Click(object sender, RoutedEventArgs e)
        {
            var speedtestClient = new SpeedTestClient();
            var result = await speedtestClient.TestSpeedAsync(SpeedUnit.MBps);

            DownloadProgress.Value = 100;
            UploadProgress.Value = 100;

            DownloadSpeed.Text = $"{result.DownloadSpeed} Мбит/с";
            UploadSpeed.Text = $"{result.UploadSpeed} Мбит/с";
        }
    }
}
