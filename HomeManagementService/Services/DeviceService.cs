#region

using AutoMapper;
using HomeManagementService.Interfaces;
using HomeManagementService.Models.Dto;
using HomeManagementService.Models.Options;
using Microsoft.Extensions.Options;

#endregion

namespace HomeManagementService.Services;

public class DeviceService : IDeviceService
{
    private readonly IOptions<Devices> _devices;
    private readonly IMapper _mapper;
    private readonly IAutomationExecutor _automationExecutor;

    public DeviceService(
        IOptions<Devices> devices,
        IMapper mapper, IAutomationExecutor automationExecutor)
    {
        _devices = devices;
        _mapper = mapper;
        _automationExecutor = automationExecutor;
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