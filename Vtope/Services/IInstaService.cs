namespace Vtope.Services;

public interface IInstaService
{
    Task<string> Login(string username, string password);
    Task Login(string sessionData);
    Task<string> PostPhoto(byte[] imageBytes);
    Task Follow(long userId);
    Task ChangeProfilePicture(byte[] imageBytes);
    Task<long> GetUserIdBy(string username);
    Task SetBio();
}