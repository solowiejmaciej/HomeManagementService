namespace HomeManagementService.Models.MqttMessages;

public record ButtonMessage
{
    public bool isOn { get; set; }
    public int timeStamp { get; set; }
}