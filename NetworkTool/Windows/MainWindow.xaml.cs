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
    private PingPage? _pingPage;
    private TraceRoutePage? _traceRoutePage;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void SideMenu_SelectionChanged(object sender, FunctionEventArgs<object> e)
    {
        var item = e.Info as SideMenuItem;

        switch (item!.Header)
        {
            case "Traceroute":
                _traceRoutePage ??= new TraceRoutePage();
                MainFrame.Navigate(_traceRoutePage);
                break;
            case "Ping":
                _pingPage ??= new PingPage();
                MainFrame.Navigate(_pingPage);
                break;
        }
    }
}