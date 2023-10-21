using Microsoft.Extensions.Options;
using ReportingServiceWorker.Interfaces;
using ReportingServiceWorker.Models.Clients;
using ReportingServiceWorker.Models.Options;

namespace ReportingServiceWorker.Factories;

public class HueClientFactory : IHueClientFactory
{
    private readonly IOptions<HueOptions> _hueOptions;

    public HueClientFactory(
        IOptions<HueOptions> hueOptions
        )
    {
        _hueOptions = hueOptions;
    }
    
    public IHueClient CreateClient() => new HueClient(_hueOptions.Value.BridgeIp, _hueOptions.Value.Key);
}


public interface IHueClientFactory
{
    IHueClient CreateClient();
}