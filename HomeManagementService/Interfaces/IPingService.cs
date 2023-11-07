#region

using HomeManagementService.Models;

#endregion

namespace HomeManagementService.Interfaces;

public interface IPingService
{
    Task<List<Device>> PingAsync();
}