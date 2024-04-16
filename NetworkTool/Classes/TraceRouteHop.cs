using System.Net;
using System.Net.NetworkInformation;

namespace NetworkTool.Classes;

public struct TraceRouteHop
{
    public int Ttl { get; set; }
    public IPAddress IpAddress { get; set; }
    public long RoundTripTime { get; set; }
    public IPStatus Status { get; set; }
}