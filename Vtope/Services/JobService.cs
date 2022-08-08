using Vtope.Models;

namespace Vtope.Services
{
    public class JobService : IJobService
    {
        private readonly IInstaService _instaService;

        public JobService(IInstaService instaService)
        {
            _instaService = instaService ?? throw new ArgumentNullException(nameof(instaService));
        }

        public async Task PrepareAccount(InstaAccount account)
        {
            // TODO change all bytes array to singleton for example
            var bytesAva = await File.ReadAllBytesAsync(@"../Files/ava.jpg");

            var bytesList = new List<byte[]>
            {
                await File.ReadAllBytesAsync(@"../Files/01.jpg"),
                await File.ReadAllBytesAsync(@"../Files/02.jpg"),
                await File.ReadAllBytesAsync(@"../Files/03.jpg")
            };

            await _instaService.Login(account.Username, account.Password);
            await Delay();

            await _instaService.SetBio();
            await Delay();

            await _instaService.ChangeProfilePicture(bytesAva);


            for (int i = 0; i < 15; i++)
            {
                await _instaService.PostPhoto(bytesList[new Random().Next(bytesList.Count)]);
                await Delay();
            }
        }

        public async Task FollowAccount(InstaAccount account, string usernameToFollow)
        {
            await _instaService.Login(account.SessionData);
            var id = await _instaService.GetUserIdBy(usernameToFollow);
            await _instaService.Follow(id);
        }

        async Task Delay()
        {
            await Task.Delay(TimeSpan.FromSeconds(new Random().Next(3, 5)));
        }
    }
}
