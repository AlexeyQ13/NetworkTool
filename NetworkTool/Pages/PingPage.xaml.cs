using System.Net;
using System.Net.NetworkInformation;
using System.Windows;

namespace NetworkTool.Pages;

/// <summary>
///     Логика взаимодействия для PingPage.xaml
/// </summary>
public partial class PingPage
{
    public PingPage()
    {
        InitializeComponent();
    }

    private async void pingButton_Click(object sender, RoutedEventArgs e)
    {
        var ip = IPAddress.Parse(IpTextBox.Text);

        // Обнуление поля вывода результата
        OutputTextBox.Text = string.Empty;

        using var ping = new Ping();
        var received = 0;
        var lost = 0;

        for (var i = 1; i <= 4; i++)
            await Task.Run(() =>
            {
                var response = ping.Send(ip, 8000);
                ++received;

                if (response.Status == IPStatus.TimedOut) ++lost;

                WriteOutput(
                    $"IP: {response.Address} | Статус: {response.Status} | Время: {response.RoundtripTime} мс");
                Thread.Sleep(1000);
            });

        WriteOutput($"\nОтправлено: {received}, получено: {received - lost}, потеряно: {lost}");
    }

    private void WriteOutput(string text)
    {
        Dispatcher.Invoke(() => { OutputTextBox.Text += text + Environment.NewLine; });
    }
}