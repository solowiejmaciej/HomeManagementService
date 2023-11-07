#region

using AutoMapper;
using HomeManagementService.Models.Dto;

#endregion

namespace HomeManagementService.Models.MappingProfiles;

public class DeviceMappingProfile : Profile
{
    public DeviceMappingProfile()
    {
        CreateMap<Device, DeviceDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
        CreateMap<DeviceDto, Device>();
    }
}