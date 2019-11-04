using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Arbor.DynamicDns
{
    public class IpFetcher
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IpFetcher(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IPAddress> GetPublicIp()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetStringAsync("https://api.ipify.org?format=json");

            var ipAddress = JsonConvert.DeserializeAnonymousType(response, new {ip = ""});

            if (IPAddress.TryParse(ipAddress.ip, out var address)) return address;

            return null;
        }
    }
}