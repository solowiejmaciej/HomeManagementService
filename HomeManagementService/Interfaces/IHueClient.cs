namespace ReportingServiceWorker.Interfaces;

public interface IHueClient
{
    public Task<bool> IsOnAsync();
}