using ReportingServiceWorker.Factories;
using ReportingServiceWorker.Interfaces;

namespace ReportingServiceWorker.Services;

public class HueService : IHueService
{
    private readonly IHueClientFactory _hueClientFactory;

    public HueService(IHueClientFactory hueClientFactory)
    {
        _hueClientFactory = hueClientFactory;
    }

    public async Task<bool> IsOnAsync()
    {
        var client = _hueClientFactory.CreateClient();
        await client.IsOnAsync();
        return true;
    }
}