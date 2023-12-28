namespace HomeManagementService.Models.MqttMessages;

public class TemperatureMessage
{
    public double temperature { get; set; }
    public int humidity { get; set; }
    public int timeStamp { get; set; }

}