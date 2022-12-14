namespace Vtope.Service;

public interface IInstaService
{
    Task<string> Login(string username, string password);
    Task Login(string sessionData);
    Task<string> PostPhoto(byte[] imageBytes);
    Task ChangeProfilePicture(byte[] imageBytes);
    Task SetBio();
}