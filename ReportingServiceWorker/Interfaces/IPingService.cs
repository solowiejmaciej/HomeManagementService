using System.Net.NetworkInformation;
using ReportingServiceWorker.Models;

namespace ReportingServiceWorker.Interfaces;

public interface IPingService
{
    Task<List<Device>> PingAsync();
}