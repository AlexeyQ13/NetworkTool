using System.Net;
using System.Windows;
using NetworkTool.Classes;
using NetworkTool.Utilities;

namespace NetworkTool.Pages;

/// <summary>
/// Логика взаимодействия для TraceRoutePage.xaml
/// </summary>
public partial class TraceRoutePage
{
    public TraceRoutePage()
    {
        InitializeComponent();
    }

    private async void PingButton_Click(object sender, RoutedEventArgs e)
    {
        var timeout = int.Parse(TimeoutTextBox.Text);
        var maxTtl = int.Parse(TtlTextBox.Text);

        // Удаление HTTPS/HTTP 
        IpTextBox.Text = IpTextBox.Text.Replace("https://", "").Replace("http://", "").TrimEnd('/');
        var address = IpTextBox.Text;

        if (!IPAddressHelper.ValidateIP(address))
        {
            MessageBox.Show("Неверный IP адрес или домен");
            IpTextBox.Text = string.Empty;
            IpTextBox.Focus();
            return;
        }

        var route = new TraceRoute(
            address: address,
            timeout: timeout,
            maxHops: maxTtl
        );
        route.HopReceived += RouteHopReceived;
        await Task.Run(() =>
        {
            var task = route.Trace();
            task.Wait();
        });
    }

    private void RouteHopReceived(object? sender, TraceRouteHop e)
    {
        WriteOutput($"{e.Ttl}\t{e.IpAddress}\t\t\t{e.RoundTripTime} мс");
    }

    private void WriteOutput(string text)
    {
        Dispatcher.Invoke(() => { OutputTextBox.AppendText(text + Environment.NewLine); });
    }
}