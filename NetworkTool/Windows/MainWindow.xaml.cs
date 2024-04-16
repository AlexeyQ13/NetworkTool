using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HandyControl.Controls;
using NetworkTool.Classes;
using NetworkTool.Pages;

namespace NetworkTool.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private PingPage? _pingPage;
        private TraceRoutePage? _traceRoutePage;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void SideMenu_SelectionChanged(object sender, HandyControl.Data.FunctionEventArgs<object> e)
        {
            SideMenuItem item = e.Info as SideMenuItem;

            switch (item.Header)
            {
                case "Traceroute":
                    if (_traceRoutePage == null)
                        _traceRoutePage = new TraceRoutePage();
                    MainFrame.Navigate(_traceRoutePage);
                    break;
                case "Ping":
                    if (_pingPage == null)
                        _pingPage = new PingPage();
                    MainFrame.Navigate(_pingPage);
                    break;
            }
        }
    }
}
