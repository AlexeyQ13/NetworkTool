using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows;
using NetworkTool.Utilities;

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

    private async void PingButton_Click(object sender, RoutedEventArgs e)
    {
        // Удаление HTTPS/HTTP 
        IpTextBox.Text = IpTextBox.Text.Replace("https://", "").Replace("http://", "").TrimEnd('/');
        var ip = IpTextBox.Text;

        if (!IPAddressHelper.ValidateIP(ip)) 
            MessageBox.Show("Неверный IP адрес или домен");

        // Обнуление поля вывода результата
        OutputTextBox.Text = string.Empty;

        using var ping = new Ping();
        var received = 0;
        var lost = 0;

        // Заголовок вывода
        WriteOutput($"{"Адрес",-16} {"Статус",-8} {"Время",-8}\n");
        
        for (var i = 1; i <= 4; i++)
            await Task.Run(() =>
            {
                var response = ping.Send(ip, 8000);
                ++received;

                if (response.Status == IPStatus.TimedOut) ++lost;

                WriteOutput($"{response.Address,-16} {response.Status,-8} {response.RoundtripTime + " мс", -8}");
                Thread.Sleep(1000);
            });

        WriteOutput($"\nОтправлено: {received}, получено: {received - lost}, потеряно: {lost}");
    }

    private void WriteOutput(string text)
    {
        Dispatcher.Invoke(() => { OutputTextBox.Text += text + Environment.NewLine; });
    }
}