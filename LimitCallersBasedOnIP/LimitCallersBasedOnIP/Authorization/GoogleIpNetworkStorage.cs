using System.Net;

namespace LimitCallersBasedOnIP.Authorization;

public class GoogleIpNetworkStorage(HttpClient httpClient, ILogger<GoogleIpNetworkStorage> logger) : IHostedService
{
    private readonly List<IPNetwork> _ipNetworks = new List<IPNetwork>();

    public bool ContainsIp(IPAddress ipAddress)
    {
        return _ipNetworks.Any(ipNetwork => ipNetwork.Contains(ipAddress));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Loading Google IP ranges");
        
            var text = await httpClient.GetStringAsync("https://www.gstatic.com/ipranges/goog.txt", cancellationToken);
            _ipNetworks.AddRange(text.GetLines().Select(line => IPNetwork.Parse(line)));
        
            logger.LogInformation("Google IP ranges loaded successfully");
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "An error occurred while attempting to load the Google IP ranges");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}