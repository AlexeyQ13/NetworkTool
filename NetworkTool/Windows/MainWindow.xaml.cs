using HandyControl.Controls;
using HandyControl.Data;
using NetworkTool.Pages;
using Window = System.Windows.Window;

namespace NetworkTool.Windows;

/// <summary>
///     Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IpInfoPage? _ipInfoPage;
    private PingPage? _pingPage;
    private TraceRoutePage? _traceRoutePage;
    private SpeedTestPage? _speedTestPage;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        _ipInfoPage = new IpInfoPage();
        MainFrame.Navigate(_ipInfoPage);

        var item = MainMenu.Items.FirstOrDefault() as SideMenuItem;
        item.IsSelected = true;
    }

    private void SideMenu_SelectionChanged(object sender, FunctionEventArgs<object> e)
    {
        var item = e.Info as SideMenuItem;

        switch (item!.Header)
        {
            case "Информация об IP":
                _ipInfoPage ??= new IpInfoPage();
                MainFrame.Navigate(_ipInfoPage);
                break;
            case "Traceroute":
                _traceRoutePage ??= new TraceRoutePage();
                MainFrame.Navigate(_traceRoutePage);
                break;
            case "Ping":
                _pingPage ??= new PingPage();
                MainFrame.Navigate(_pingPage);
                break;
            case "SpeedTest":
                _speedTestPage ??= new SpeedTestPage();
                MainFrame.Navigate(_speedTestPage);
                break;
            case "WHOIS":
                break;
        }
    }
}