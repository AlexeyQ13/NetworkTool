using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Classes
{
    public struct TraceRouteHop
    {
        public int TTL { get; set; }
        public IPAddress IPAddress { get; set; }
        public long RoundTripTime { get; set; }
        public IPStatus Status { get; set; }
    }
}
