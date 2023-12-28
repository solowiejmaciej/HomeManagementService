using System.Text;
using HomeManagementService.Interfaces;
using HomeManagementService.Models;
using HomeManagementService.Models.MqttMessages;
using HomeManagementService.Models.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace HomeManagementService.Workers;

public class MqttKnobWorker : BackgroundService
{
    private readonly IHueService _hueService;
    private readonly IOptions<MqttOptions> _mqttOptions;
    private readonly TemperatureInfo _temperatureInfo;

    public MqttKnobWorker(
        IHueService hueService,
        IOptions<MqttOptions> mqttOptions,
        TemperatureInfo temperatureInfo
        )
    {
        _hueService = hueService;
        _mqttOptions = mqttOptions;
        _temperatureInfo = temperatureInfo;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var brokerAddress = _mqttOptions.Value.BrokerAddress;
        var brokerPort = _mqttOptions.Value.BrokerPort;
        var buttonTopic = _mqttOptions.Value.ButtonTopic;
        var temperatureTopic = _mqttOptions.Value.TemperatureTopic;
        var rotaryTopic = _mqttOptions.Value.RotaryTopic;
        var clientId = _mqttOptions.Value.ClientId;
        
        MqttClient client = new MqttClient(brokerAddress, brokerPort, false, MqttSslProtocols.None, null, null);

        client.Connect(clientId);
        
        client.Subscribe(new string[] 
                { buttonTopic, temperatureTopic, rotaryTopic, }, 
            new byte[] { 
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, 
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE
            }
        );

        client.MqttMsgPublishReceived += (sender, e) =>
        {

            if (e.Topic == buttonTopic)
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
            
            if (e.Topic == rotaryTopic)
            {
                var message = Encoding.UTF8.GetString(e.Message);
                var meessageInJson = JsonConvert.DeserializeObject<RotaryMessage>(message);
                if (meessageInJson.value > 0)
                {
                    _hueService.SetBrightnessToAllAsync(meessageInJson.value).Wait(stoppingToken);
                }

            }
            
            if (e.Topic == temperatureTopic)
            {
                var message = Encoding.UTF8.GetString(e.Message);
                var meessageInJson = JsonConvert.DeserializeObject<TemperatureMessage>(message);
                _temperatureInfo.Temperature = meessageInJson.temperature;
                _temperatureInfo.Humidity = meessageInJson.humidity;
                _temperatureInfo.LastChanged = meessageInJson.timeStamp;
            }
            
            //Console.WriteLine($"Received message on topic '{e.Topic}': {Encoding.UTF8.GetString(e.Message)}");
        };
        
    }
}