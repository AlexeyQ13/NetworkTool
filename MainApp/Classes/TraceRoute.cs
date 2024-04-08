using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace MainApp.Classes
{
    class TraceRoute
    {
        private IPAddress _destinationIPAddress;
        private int _timeout;
        private int _maxHops;

        private static byte[] _buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

        public event EventHandler<TraceRouteHop>? HopReceived;

        public TraceRoute(IPAddress destinationIPAddress, int maxHops = 30, int timeout = 10000)
        {
            _destinationIPAddress = destinationIPAddress;
            _timeout = timeout;
            _maxHops = maxHops;
        }

        private async Task<TraceRouteHop> TraceRouteAsync(int ttl)
        {
            Ping ping = new Ping();

            PingOptions pingOptions = new PingOptions
            {
                Ttl = ttl,
            };

            PingReply response = await ping.SendPingAsync(_destinationIPAddress, _timeout, _buffer, pingOptions);

            return new TraceRouteHop
            {
                TTL = ttl,
                IPAddress = response.Address,
                RoundTripTime = response.RoundtripTime,
                Status = response.Status,
            };
        }

        public async Task Trace()
        {
            for (int ttl = 1; ttl <= _maxHops; ttl++)
            {
                TraceRouteHop hop = await TraceRouteAsync(ttl);
                Task.WaitAll();
                HopReceived?.Invoke(this, hop);
                
                if (IPAddress.Equals(hop.IPAddress, _destinationIPAddress))
                {
                    break;
                }

            }
        }
    }
}
