using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using NetworkTool.Utilities;

namespace NetworkTool.Pages;

/// <summary>
///     Логика взаимодействия для IpInfoPage.xaml
/// </summary>
public partial class IpInfoPage : Page
{
    public IpInfoPage()
    {
        InitializeComponent();
        DataContext = this;

        Init();
    }

    public async void Init()
    {
        IpTextBox.Text = IPAddressHelper.GetPublicIP();
    }

    private async void ExecButton_OnClick(object sender, RoutedEventArgs e)
    {
        // Обнуление поля вывода результата
        OutputTextBox.Text = string.Empty;

        // Удаление HTTPS/HTTP 
        IpTextBox.Text = IpTextBox.Text.Replace("https://", "").Replace("http://", "").TrimEnd('/');
        var ip = IpTextBox.Text;

        if (!IPAddressHelper.ValidateIP(ip))
        {
            MessageBox.Show("Неверный IP адрес или домен");
            return;
        }

        var ipInfo = await IpInfo.GetIpInfoAsync(ip);
        OutputTextBox.Text = ipInfo.ToString();
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