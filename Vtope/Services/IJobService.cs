using Vtope.Domain;

namespace Vtope.Services;

public interface IJobService
{
    Task PrepareAccount(InstaAccount account);
}