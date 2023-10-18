using System.Diagnostics;
using System.Net.NetworkInformation;

namespace ReportingServiceWorker.Models;

public class Device
{
    public Device() => StartClock();

    public required string Alias { get; set; }
    public required string Ip { get; set; }
    public EDeviceState Status { get; private set; }
    public EDeviceState PreviousStatus { get; private set; } = EDeviceState.Unknown;
    public TimeSpan ElapsedTime => GetElapsedTime();
    public DateTime LastDateStatusChanged { get; private set; }
    private readonly Stopwatch _stopWatch = new();

    private void StartClock() => _stopWatch.Start();

    private TimeSpan GetElapsedTime() => _stopWatch.Elapsed;
    
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
        Ping ping = new Ping();
        try
        {
            var result = await ping.SendPingAsync(Ip);
            if (result.Status == IPStatus.Success)
            {
                SetStatusToOnline();
            }
            else
            {
                SetStatusToOffline();
            }
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