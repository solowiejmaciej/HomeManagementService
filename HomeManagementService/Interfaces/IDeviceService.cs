#region

using HomeManagementService.Models.Dto;

#endregion

namespace HomeManagementService.Interfaces;

public interface IDeviceService
{
    Task<List<DeviceDto>> GetAll();
    Task<DeviceDto> GetByIdAsync(int id);
}