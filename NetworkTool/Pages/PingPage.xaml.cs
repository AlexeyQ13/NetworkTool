using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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

namespace NetworkTool.Pages
{
    /// <summary>
    /// Логика взаимодействия для PingPage.xaml
    /// </summary>
    public partial class PingPage : Page
    {
        public PingPage()
        {
            InitializeComponent();
        }

        private async void pingButton_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse(ipTextBox.Text);

            // Обнуление поля вывода результата
            outputTextBox.Text = string.Empty;

            using (Ping ping = new Ping())
            {
                int received = 0;
                int lost = 0;

                for (int i = 1; i <= 4; i++)
                {
                    await Task.Run(() =>
                    {
                        PingReply response = ping.Send(ip, 8000);
                        ++received;

                        if (response.Status == IPStatus.TimedOut)
                        {
                            ++lost;
                        }

                        WriteOutput($"IP: {response.Address} | Статус: {response.Status} | Время: {response.RoundtripTime} мс");
                        Thread.Sleep(1000);
                    });
                }

                WriteOutput($"\nОтправлено: {received}, получено: {received - lost}, потеряно: {lost}");

            }

        }

        private void WriteOutput(string text)
        {
            Dispatcher.Invoke(() =>
            {
                outputTextBox.Text += (text + Environment.NewLine);
            });
        }
    }
}
