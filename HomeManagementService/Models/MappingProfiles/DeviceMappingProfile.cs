using AutoMapper;
using ReportingServiceWorker.Models.Dto;
using ReportingServiceWorker.Models.Options;

namespace ReportingServiceWorker.Models.MappingProfiles;

public class DeviceMappingProfile : Profile
{
    public DeviceMappingProfile()
    {
        CreateMap<Device, DeviceDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
        CreateMap<DeviceDto, Device>();
    }
}