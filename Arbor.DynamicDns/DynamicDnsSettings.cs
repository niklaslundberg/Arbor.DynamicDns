using System;

namespace Arbor.DynamicDns
{
    public class DynamicDnsSettings
    {
        public Uri BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
    }
}