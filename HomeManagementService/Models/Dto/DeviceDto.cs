namespace ReportingServiceWorker.Models.Dto;

public class DeviceDto
{
    public required int Id { get; set; }
    public required string Alias { get; set; }
    public required string IpAddress { get; set; }
    public EDeviceState Status { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public DateTime LastDateStatusChanged { get; set; }

}