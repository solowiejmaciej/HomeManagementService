namespace ReportingServiceWorker.Interfaces;

public interface IHueService
{
    Task<bool> IsOnAsync();
}