namespace ReportingServiceWorker.Interfaces;

public interface INotificationApiClient
{
    public void SendSms(string userId, string message);
}