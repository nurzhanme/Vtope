namespace Vtope.Service;

public interface IJobService
{
    Task PrepareAccount(string username, string password, string sessionState = default);
}