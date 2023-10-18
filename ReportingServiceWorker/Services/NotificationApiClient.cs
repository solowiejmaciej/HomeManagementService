using ReportingServiceWorker.Interfaces;

namespace ReportingServiceWorker.Services;

public class NotificationApiClient : INotificationApiClient
{
    public void SendSms(string userId, string message)
    {
        Console.WriteLine($"Sending SMS to {userId} with message: {message}");
    }
}