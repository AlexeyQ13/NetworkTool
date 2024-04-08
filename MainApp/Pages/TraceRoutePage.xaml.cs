using System;
using System.Collections.Generic;
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
using MainApp.Classes;

namespace MainApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для TraceRoutePage.xaml
    /// </summary>
    public partial class TraceRoutePage : Page
    {
        public TraceRoutePage()
        {
            InitializeComponent();
        }

        private void pingButton_Click(object sender, RoutedEventArgs e)
        {
            var ipAddressTextBox = ipTextBox.Text;
            int timeout = int.Parse(timeoutTextBox.Text);
            int maxTtl = int.Parse(ttlTextBox.Text);

            if (!IPAddressHelper.ValidateIP(ipAddressTextBox))
            {
                HandyControl.Controls.MessageBox.Show("Ошибка IPAddress адреса");
                ipTextBox.Text = string.Empty;
                ipTextBox.Focus();
                return;
            }

            var ipAddress = IPAddress.Parse(ipAddressTextBox);
            //using (Ping ping = new Ping())
            //{
            //    await Task.Run(() =>
            //    {
            //        PingReply response = ping.Send(ipAddress, 8000);
            //        WriteOutput($"IPAddress: {response.Address} | Статус: {response.Status} | Время: {response.RoundtripTime} мс");

            //    });
            //}

            TraceRoute route = new TraceRoute(
                destinationIPAddress: ipAddress,
                timeout: timeout,
                maxHops: maxTtl
            );
            route.HopReceived += RouteHopReceived;
            Task.Run(() =>
            {
                Task task = route.Trace();
                task.Wait();
            });
            
        }

        private void RouteHopReceived(object? sender, TraceRouteHop e)
        {
            WriteOutput($"{e.TTL}\t{e.IPAddress}\t\t\t{e.RoundTripTime} мс");
        }

        private void WriteOutput(string text)
        {
            Dispatcher.Invoke(() =>
            {
                outputTextBox.AppendText(text + Environment.NewLine);
            });
        }
    }
}
