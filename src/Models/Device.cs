#region

using System.Diagnostics;
using System.Net.NetworkInformation;

#endregion

namespace HomeManagementService.Models;

public class Device
{
    public Device()
    {
        StartClock();
    }

    public required int Id { get; set; }
    public required string MacAddress { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Alias { get; set; }
    public required string IpAddress { get; set; }
    public EDeviceState Status { get; private set; }
    public EDeviceState PreviousStatus { get; private set; } = EDeviceState.Unknown;
    public TimeSpan ElapsedTime => GetElapsedTime();
    public DateTime LastDateStatusChanged { get; private set; }
    private readonly Stopwatch _stopWatch = new();

    private void StartClock()
    {
        _stopWatch.Start();
    }

    private TimeSpan GetElapsedTime()
    {
        return _stopWatch.Elapsed;
    }

    private void SetStatusToOnline()
    {
        PreviousStatus = Status;
        if (Status == EDeviceState.Online) return;
        Status = EDeviceState.Online;
        _stopWatch.Restart();
        LastDateStatusChanged = DateTime.Now;
    }


    private void SetStatusToOffline()
    {
        PreviousStatus = Status;
        if (Status == EDeviceState.Offline) return;
        Status = EDeviceState.Offline;
        _stopWatch.Restart();
        LastDateStatusChanged = DateTime.Now;
    }

    public async Task PingAsync()
    {
        var ping = new Ping();
        try
        {
            var result = await ping.SendPingAsync(IpAddress);
            if (result.Status == IPStatus.Success)
                SetStatusToOnline();
            else
                SetStatusToOffline();
        }
        catch (Exception e)
        {
            // ignored
        }
        finally
        {
            ping.Dispose();
        }
    }

    public override string ToString()
    {
        return $"{Alias} {Status} LAST_CHANGE: {LastDateStatusChanged}";
    }
}

public enum EDeviceState
{
    Unknown,
    Online,
    Offline
}