namespace HomeManagementService.Workers;

public record ButtonMessage
{
    public bool isOn { get; set; }
    public int timeStamp { get; set; }
}