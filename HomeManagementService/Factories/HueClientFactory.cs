#region

using HomeManagementService.Models.Options;
using HueApi;
using Microsoft.Extensions.Options;

#endregion

namespace HomeManagementService.Factories;

public class HueClientFactory : IHueClientFactory
{
    private readonly IOptions<HueOptions> _hueOptions;

    public HueClientFactory(
        IOptions<HueOptions> hueOptions
    )
    {
        _hueOptions = hueOptions;
    }

    public LocalHueApi CreateClient()
    {
        return new LocalHueApi(_hueOptions.Value.BridgeIp, _hueOptions.Value.Key);
    }
}

public interface IHueClientFactory
{
    LocalHueApi CreateClient();
}