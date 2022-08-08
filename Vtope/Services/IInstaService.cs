namespace Vtope.Services;

public interface IInstaService
{
    Task<string> Login(string username, string password);
    Task Login(string sessionData);
    Task<string> PostPhoto(string imageUrl, string caption);
    Task<string> PostPhoto(byte[] imageBytes, string caption);
    Task Follow(long userId);
    Task ChangeProfilePicture(byte[] imageBytes);
    Task SetBio(string mediaId);
}