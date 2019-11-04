using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Arbor.DynamicDns
{
    public class DynamicDnsClient
    {
        private readonly DynamicDnsSettings _dynamicDnsSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public DynamicDnsClient(IHttpClientFactory httpClientFactory, DynamicDnsSettings dynamicDnsSettings,
            ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _dynamicDnsSettings = dynamicDnsSettings;
            _logger = logger;
        }

        public async Task<UpdateResult> Update(IPAddress ipAddress, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(_dynamicDnsSettings.BaseUrl)
            {
                Query =
                    $"hostname={Uri.EscapeDataString(_dynamicDnsSettings.HostName)}&myip={Uri.EscapeDataString(ipAddress.ToString())}"
            };

            var requestUri = uriBuilder.Uri;

            var byteArray = Encoding.UTF8.GetBytes($"{_dynamicDnsSettings.Username}:{_dynamicDnsSettings.Password}");

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var client = _httpClientFactory.CreateClient();
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var httpResponseMessage = await client.SendAsync(request, cancellationToken);

            string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            _logger.Information("Http response status code: {StatusCode}, body {Body}", httpResponseMessage.StatusCode,
                responseBody);

            return new UpdateResult(responseBody);
        }
    }
}