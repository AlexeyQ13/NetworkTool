using System.Net;
using System.Text.RegularExpressions;

namespace NetworkTool.Utilities;

public class IPAddressHelper
{
    /* Проверяет, является ли IP-адрес частным.*/

    public static bool IsPrivateIPAddress(IPAddress ipAddress)
    {
        var addressBytes = ipAddress.GetAddressBytes();

        switch (addressBytes.Length)
        {
            // ipv4
            case 4 when addressBytes[0] == 10
                        || (addressBytes[0] == 172 && addressBytes[1] >= 16 && addressBytes[1] <= 31)
                        || (addressBytes[0] == 192 && addressBytes[1] == 168):
            // ipv6
            case 16 when addressBytes[0] == 0xFC || addressBytes[0] == 0xFD:
                return true;
            default:
                return false;
        }
    }

    /* Получение публичного IP путем запроса на сайт ipinfo */

    public static async Task<IPAddress> GetPublicIP(string serviceUrl = "https://ipinfo.io/ip")
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(serviceUrl);
        return IPAddress.Parse(response);
    }

    /* Проверка адреса на "вшивость" */

    public static bool ValidateIP(string ip)
    {
        return Regex.IsMatch(ip, "^((25[0-5]|(2[0-4]|1\\d|[1-9]|)\\d)\\.?\\b){4}$");
    }

    /* Сравнение двух адресов */

    public static int CompareIPAddresses(IPAddress x, IPAddress y)
    {
        return ByteHelper.Compare(x.GetAddressBytes(), y.GetAddressBytes());
    }
}