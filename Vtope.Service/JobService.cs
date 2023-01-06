namespace Vtope.Service;

public class JobService : IJobService
{
    private readonly IInstaService _instaService;

    public JobService(IInstaService instaService)
    {
        _instaService = instaService ?? throw new ArgumentNullException(nameof(instaService));
    }

    public async Task PrepareAccount(string username, string password, string sessionState = default)
    {
        // TODO change all bytes array to singleton for example
        var bytesAva = await File.ReadAllBytesAsync(@"../Files/ava.jpg");
        var bytes = await File.ReadAllBytesAsync(@"../Files/01.jpg");

        if (string.IsNullOrEmpty(sessionState))
        {
            await _instaService.Login(username, password);
        }
        else
        {
            await _instaService.Login(sessionState);
        }


        await _instaService.SetBio();
        await Delay();

        await _instaService.ChangeProfilePicture(bytesAva);


        for (int i = 0; i < 15; i++)
        {
            await _instaService.PostPhoto(bytes);
            await Delay();
        }
    }

    async Task Delay()
    {
        await Task.Delay(TimeSpan.FromSeconds(new Random().Next(3, 5)));
    }
}