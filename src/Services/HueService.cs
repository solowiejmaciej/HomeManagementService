#region

using System.Drawing;
using HomeManagementService.Factories;
using HomeManagementService.Interfaces;
using HueApi;
using HueApi.ColorConverters;
using HueApi.ColorConverters.Original.Extensions;
using HueApi.Models;
using HueApi.Models.Requests;

#endregion

namespace HomeManagementService.Services;

public class HueService : IHueService
{
    private readonly IHueClientFactory _hueClientFactory;
    private readonly LocalHueApi _hueClient;

    public HueService(IHueClientFactory hueClientFactory)
    {
        _hueClientFactory = hueClientFactory;
        _hueClient = _hueClientFactory.CreateClient();
    }

    private async Task<HueResponse<Light>> GetLightsAsync()
    {
        return await _hueClient.GetLightsAsync();
    }

    private int GetOldHueIdFromString(string id)
    {
        var lights = GetLightsAsync().Result;
        var light = lights.Data.FirstOrDefault(l => l.IdV1 != null && id == l.IdV1);
        return int.Parse(light.IdV1.Last().ToString());
    }

    public async Task TurnOffLightsAsync()
    {
        var lights = await GetLightsAsync();
        foreach (var light in lights.Data)
        {
            var req = new UpdateLight()
                .TurnOff();
            var result = await _hueClient.UpdateLightAsync(light.Id, req);
        }
    }

    public async Task TurnOffLightAsync(int lightId)
    {
        var lights = await GetLightsAsync();
        var light = lights.Data.FirstOrDefault(l => lightId == GetOldHueIdFromString(l.IdV1));
        var req = new UpdateLight()
            .TurnOff();
        await _hueClient.UpdateLightAsync(light.Id, req);
    }

    public async Task TurnOnLightsAsync()
    {
        var lights = await GetLightsAsync();
        foreach (var light in lights.Data)
        {
            var req = new UpdateLight()
                .TurnOn();
            var result = await _hueClient.UpdateLightAsync(light.Id, req);
        }
    }

    public async Task TurnOnLightAsync(int lightId)
    {
        var lights = await GetLightsAsync();
        var light = lights.Data.FirstOrDefault(l => lightId == GetOldHueIdFromString(l.IdV1));
        var req = new UpdateLight()
            .TurnOn();
        await _hueClient.UpdateLightAsync(light.Id, req);
    }

    public async Task SetBrightnessToAllAsync(int brightness)
    {
        var lights = await GetLightsAsync();
        foreach (var light in lights.Data)
        {
            var req = new UpdateLight()
                .SetBrightness(brightness)
                .TurnOn();
            await _hueClient.UpdateLightAsync(light.Id, req);
        }
    }

    public async Task SetBrightnessAsync(int brightness, int lightId)
    {
        var lights = await GetLightsAsync();
        var light = lights.Data.FirstOrDefault(l => lightId == GetOldHueIdFromString(l.IdV1));
        var req = new UpdateLight()
            .SetBrightness(brightness)
            .TurnOn();
        await _hueClient.UpdateLightAsync(light.Id, req);
    }

    public async Task SetColorToAllAsync(string color)
    {
        var rgbColor = ColorTranslator.FromHtml(color);

        var lights = await GetLightsAsync();
        foreach (var light in lights.Data)
        {
            var req = new UpdateLight()
                .TurnOn()
                .SetColor(new RGBColor
                {
                    R = rgbColor.R,
                    G = rgbColor.G,
                    B = rgbColor.B
                });
            await _hueClient.UpdateLightAsync(light.Id, req);
        }
    }

    public async Task SetColorAsync(string color, int lightId)
    {
        var rgbColor = ColorTranslator.FromHtml(color);

        var lights = await GetLightsAsync();
        var light = lights.Data.FirstOrDefault(l => lightId == GetOldHueIdFromString(l.IdV1));
        var req = new UpdateLight()
            .TurnOn()
            .SetColor(new RGBColor
            {
                R = rgbColor.R,
                G = rgbColor.G,
                B = rgbColor.B
            });
        await _hueClient.UpdateLightAsync(light.Id, req);
    }
}