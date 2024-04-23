using System.Windows;
using System.Windows.Controls;
using NetworkTool.Utilities;

namespace NetworkTool.Pages;

/// <summary>
///     Логика взаимодействия для SpeedTestPage.xaml
/// </summary>
public partial class SpeedTestPage : Page
{
    public SpeedTestPage()
    {
        InitializeComponent();
    }

    private async void RunButton_OnClick(object sender, RoutedEventArgs e)
    {
        var prevButtonText = RunButton.Content;
        RunButton.IsEnabled = false;
        RunButton.Content = "Ожидайте ...";

        var client = new SpeedTest();
        await client.Start();
        DownloadSpeedTextBlock.Text = $"{client.DownloadSpeed.ToString()} МБит/сек";

        RunButton.IsEnabled = true;
        RunButton.Content = prevButtonText;
    }
}