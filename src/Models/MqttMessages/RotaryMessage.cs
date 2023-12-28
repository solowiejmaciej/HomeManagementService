namespace HomeManagementService.Models.MqttMessages;

public record RotaryMessage
{
    public int value { get; set; }
    public int timeStamp { get; set; }
}