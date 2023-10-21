using AutoMapper;
using Microsoft.Extensions.Options;
using ReportingServiceWorker.Interfaces;
using ReportingServiceWorker.Models.Dto;
using ReportingServiceWorker.Models.Options;

namespace ReportingServiceWorker.Services;

public class DeviceService : IDeviceService
{
    private readonly IOptions<Devices> _devices;
    private readonly IMapper _mapper;

    public DeviceService(
        IOptions<Devices> devices,
        IMapper mapper
        )
    {
        _devices = devices;
        _mapper = mapper;
    }
    public Task<List<DeviceDto>> GetAll()
    {
        var configuredDevices = _devices.Value.List;
        var dtos = _mapper.Map<List<DeviceDto>>(configuredDevices);
        return Task.FromResult(dtos);
    }

    public Task<DeviceDto> GetByIdAsync(int id)
    {
        var configuredDevices = _devices.Value.List;
        var device = configuredDevices.FirstOrDefault(d => d.Id == id);
        var dto = _mapper.Map<DeviceDto>(device);
        return Task.FromResult(dto);
    }
}
