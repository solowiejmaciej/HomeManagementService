using Flurl.Http;
using ReportingServiceWorker.Interfaces;

namespace ReportingServiceWorker.Models.Clients;

public sealed class HueClient : IHueClient
{
    private string Key { get; set; }
    private string Ip { get; set; }
    private string BaseUrl => $"http://{Ip}/api/{Key}/";
    public HueClient(string ip, string key)
    {
        Ip = ip;
        Key = key;
    }
    
    public async Task<bool> IsOnAsync()
    {
        var result = await $"{BaseUrl}".GetJsonAsync<dynamic>();
        Console.WriteLine(result);
        return true;
    }
}
