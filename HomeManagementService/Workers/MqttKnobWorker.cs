using System.Text;
using HomeManagementService.Interfaces;
using HomeManagementService.Models;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace HomeManagementService.Workers;

public class MqttKnobWorker : BackgroundService
{
    private readonly IHueService _hueService;

    public MqttKnobWorker(
        IHueService hueService
        )
    {
        _hueService = hueService;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Define your MQTT broker address and port
        string brokerAddress = "192.168.1.138";
        int brokerPort = 1883; // Default MQTT port

        // Create an MQTT client instance
        MqttClient client = new MqttClient(brokerAddress, brokerPort, false, MqttSslProtocols.None, null, null);

        // Specify your MQTT client ID
        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);

        // Subscribe to the MQTT topic(s) you are interested in
        string[] topics = { "/home/sensors/button", "/home/sensors/rotory" };
        byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE };
        client.Subscribe(topics, qosLevels);

        // Define a callback for handling received messages
        client.MqttMsgPublishReceived += (sender, e) =>
        {

            if (e.Topic == "/home/sensors/button")
            {
                var message = Encoding.UTF8.GetString(e.Message);
                var meessageInJson = JsonConvert.DeserializeObject<ButtonMessage>(message);
                if (!meessageInJson.isOn)
                {
                    _hueService.TurnOffLightsAsync();
                }
                else
                {
                    _hueService.TurnOnLightsAsync();
                }
            }
            
            if (e.Topic == "/home/sensors/rotory")
            {
                var message = Encoding.UTF8.GetString(e.Message);
                var meessageInJson = JsonConvert.DeserializeObject<RotaryMessage>(message);
                if (meessageInJson.value > 0)
                {
                    _hueService.SetBrightnessToAllAsync(meessageInJson.value).Wait(stoppingToken);
                }

            }
            
            Console.WriteLine($"Received message on topic '{e.Topic}': {Encoding.UTF8.GetString(e.Message)}");
        };
        
    }
}