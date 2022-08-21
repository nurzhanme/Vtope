using Vtope.Domain;
using Vtope.Models;

namespace Vtope.Services;

public interface IJobService
{
    Task PrepareAccount(InstaAccount account);
    Task FollowAccount(InstaAccount account, string usernameToFollow);
}