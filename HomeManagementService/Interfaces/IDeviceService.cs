using ReportingServiceWorker.Models.Dto;

namespace ReportingServiceWorker.Interfaces;

public interface IDeviceService
{
    Task<List<DeviceDto>> GetAll();
    Task<DeviceDto> GetByIdAsync(int id);
}