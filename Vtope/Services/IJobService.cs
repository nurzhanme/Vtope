using Vtope.Models;

namespace Vtope.Services;

public interface IJobService
{
    Task PrepareAccount(InstaAccount account);
    Task PrepareAccount(InstaAccount account, string usernameToFollow);
}