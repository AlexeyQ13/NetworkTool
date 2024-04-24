using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NetworkTool.Utilities;

namespace NetworkTool.Pages
{
    /// <summary>
    /// Логика взаимодействия для IpInfoPage.xaml
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

            IpInfo? ipInfo = await IpInfo.GetIpInfoAsync(ip);
            OutputTextBox.Text = ipInfo.ToString();
        }
    }
}
