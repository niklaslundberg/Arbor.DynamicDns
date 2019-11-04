using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Arbor.DynamicDns
{
    public class DynamicDnsBackgroundService : BackgroundService
    {
        private readonly DynamicDnsClient _dynamicDnsClient;
        private readonly IpFetcher _ipFetcher;
        private readonly ILogger _logger;

        private CancellationToken _stoppingToken;

        public DynamicDnsBackgroundService(IpFetcher ipFetcher, DynamicDnsClient dynamicDnsClient, ILogger logger)
        {
            _ipFetcher = ipFetcher;
            _dynamicDnsClient = dynamicDnsClient;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stoppingToken = stoppingToken;
            return Task.Run(Run, stoppingToken);
        }

        private async Task Run()
        {
            while (!_stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var ipAddress = await _ipFetcher.GetPublicIp();

                    if (ipAddress is {})
                    {
                        await _dynamicDnsClient.Update(ipAddress, _stoppingToken);
                        _logger.Information("Successfully updated public IP {Address}", ipAddress);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Could not update IP address");
                }

                await Task.Delay(TimeSpan.FromSeconds(60), _stoppingToken);
            }
        }
    }
}