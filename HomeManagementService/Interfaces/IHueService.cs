namespace HomeManagementService.Interfaces;

public interface IHueService
{
    Task TurnOffLightsAsync();
    Task TurnOffLightAsync(int lightId);
    Task TurnOnLightsAsync();
    Task TurnOnLightAsync(int lightId);
    Task SetBrightnessToAllAsync(int brightness);
    Task SetBrightnessAsync(int brightness, int lightId);
    Task SetColorToAllAsync(string color);
    Task SetColorAsync(string color, int lightId);
}