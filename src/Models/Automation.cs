namespace HomeManagementService.Models;

public record Automation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public bool IsEnabled { get; set; } = true;
    public bool IsLocationBased { get; set; }
    public bool IsTimeBased { get; set; }
    public string? TriggerLat { get; set; }
    public string? TriggerLong { get; set; }
    public string? Cron { get; set; }
    public List<ActionRequest> ActionRequest { get; set; }
}

public record ActionRequest
{
    public string Url { get; set; }
    public string Method { get; set; }
    public string? Body { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public Dictionary<string, string>? QueryParams { get; set; }
}