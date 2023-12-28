namespace HomeManagementService.Models.Options;

public class MqttOptions
{
    public string BrokerAddress { get; set; }
    public int BrokerPort { get; set; }
    public string ClientId { get; set; }
    public string TemperatureTopic { get; set; }
    public string ButtonTopic { get; set; }
    public string RotaryTopic { get; set; }
}