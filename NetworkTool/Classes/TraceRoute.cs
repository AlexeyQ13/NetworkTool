using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace NetworkTool.Classes;

internal class TraceRoute
{
    private readonly string _address;
    private readonly int _timeout;
    private readonly int _maxHops;

    private static readonly byte[] Buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

    public event EventHandler<TraceRouteHop>? HopReceived;

    public TraceRoute(string address, int maxHops = 30, int timeout = 10000)
    {
        _address = address;
        _timeout = timeout;
        _maxHops = maxHops;
    }

    private async Task<TraceRouteHop> TraceRouteAsync(int ttl)
    {
        var ping = new Ping();

        var pingOptions = new PingOptions
        {
            Ttl = ttl
        };

        var response = await ping.SendPingAsync(_address, _timeout, Buffer, pingOptions);

        return new TraceRouteHop
        {
            Ttl = ttl,
            IpAddress = response.Address,
            RoundTripTime = response.RoundtripTime,
            Status = response.Status
        };
    }

    public async Task Trace()
    {
        var ping = new Ping();
        var response = await ping.SendPingAsync(_address);

        var ip = response.Address;

        for (var ttl = 1; ttl <= _maxHops; ttl++)
        {
            var hop = await TraceRouteAsync(ttl);
            Task.WaitAll();
            HopReceived?.Invoke(this, hop);

            if (Equals(hop.IpAddress, ip)) break;
        }
    }
}