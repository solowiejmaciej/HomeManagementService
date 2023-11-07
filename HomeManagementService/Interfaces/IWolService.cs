namespace HomeManagementService.Interfaces;

public interface IWolService
{
    Task WakeUpAsync(int id);
    Task Shutdown(int id);
}