using System.IO;
using System.Net;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Input;
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

    private void OutputTextBox_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        var menu = new ContextMenu();

        var copyMenuItem = new MenuItem
        {
            Header = "Скопировать"
        };
        copyMenuItem.Click += CopyText;

        menu.Items.Add(copyMenuItem);

        var exportMenuItem = new MenuItem
        {
            Header = "Экспортировать"
        };
        exportMenuItem.Click += ExportResults;
        menu.Items.Add(exportMenuItem);

        OutputTextBox.ContextMenu = menu;
    }

    private void ExportResults(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog
        {
            DefaultExt = ".txt",
            Filter = "Текстовый документ (.txt)|*.txt"
        };

        if (dialog.ShowDialog() != true) return;
        var filename = dialog.FileName;
        File.WriteAllText(filename, OutputTextBox.Text);
    }

    private void CopyText(object sender, RoutedEventArgs e)
    {
        if (OutputTextBox.SelectedText != string.Empty)
        {
            Clipboard.SetText(OutputTextBox.SelectedText);
        }
    }
}