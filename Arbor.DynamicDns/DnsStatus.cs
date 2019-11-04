using System;

namespace Arbor.DynamicDns
{
    public class DnsStatus
    {
        public DateTime UpdatedAtUtc { get; set; }

        public string LatestResponse { get; set; }

        public string LatestIpAddress { get; set; }
    }
}