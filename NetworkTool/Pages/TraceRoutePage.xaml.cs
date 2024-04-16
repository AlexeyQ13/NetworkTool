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

    private void PingButton_Click(object sender, RoutedEventArgs e)
    {
        var ipAddressTextBox = IpTextBox.Text;
        var timeout = int.Parse(TimeoutTextBox.Text);
        var maxTtl = int.Parse(TtlTextBox.Text);

        if (!IPAddressHelper.ValidateIP(ipAddressTextBox))
        {
            HandyControl.Controls.MessageBox.Show("Ошибка IPAddress адреса");
            IpTextBox.Text = string.Empty;
            IpTextBox.Focus();
            return;
        }

        var ipAddress = IPAddress.Parse(ipAddressTextBox);

        var route = new TraceRoute(
            ipAddress,
            timeout: timeout,
            maxHops: maxTtl
        );
        route.HopReceived += RouteHopReceived;
        Task.Run(() =>
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