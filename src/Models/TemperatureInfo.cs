namespace HomeManagementService.Models;

public class TemperatureInfo
{
    public double Temperature { get; set; }
    public int Humidity { get; set; }
    public int LastChanged { get; set; }
}