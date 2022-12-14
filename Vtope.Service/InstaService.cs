using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using Vtope.Service.Exceptions;

namespace Vtope.Service;

public class InstaService : IInstaService
{
    private IInstaApi? _instaApi;

    readonly List<string> _bios = new()
    {
        "Live", "Life", "Love", "Hot", "Top"
    };
    public async Task<string> Login(string username, string password)
    {

        _instaApi = InstaApiBuilder.CreateBuilder()
            .SetUser(UserSessionData.ForUsername(username).WithPassword(password))
            .SetRequestDelay(RequestDelay.FromSeconds(2, 2))
            .Build();

        await _instaApi.SendRequestsBeforeLoginAsync();
        var result = await _instaApi.LoginAsync();

        if (!result.Succeeded || !_instaApi.IsUserAuthenticated)
        {
            throw new InstaException(result.Info.Message);
        }

        await _instaApi.SendRequestsAfterLoginAsync();

        var stateData = await _instaApi.GetStateDataAsStringAsync();
        return stateData;
    }

    public async Task Login(string sessionData)
    {

        _instaApi = InstaApiBuilder.CreateBuilder()
            .SetUser(UserSessionData.Empty)
            .SetRequestDelay(RequestDelay.FromSeconds(2, 2))
            .Build();

        await _instaApi.LoadStateDataFromStringAsync(sessionData);

        if (!_instaApi.IsUserAuthenticated) throw new InstaException("Not Authorized");
    }

    public async Task<string> PostPhoto(byte[] imageBytes)
    {

        var image = new InstaImageUpload { ImageBytes = imageBytes };

        var result = await _instaApi.MediaProcessor.UploadPhotoAsync(image, GetRandomMessage());

        if (!result.Succeeded)
        {
            if (result.Info.ResponseType == ResponseType.CheckPointRequired)
            {
                await _instaApi.RequestVerifyCodeToEmailForChallengeRequireAsync();
            }

            throw new InstaException(result.Info.Message);
        }

        return result.Value.Pk;

    }

    public async Task ChangeProfilePicture(byte[] imageBytes)
    {
        var result = await _instaApi.AccountProcessor.ChangeProfilePictureAsync(imageBytes);

        if (!result.Succeeded)
        {
            throw new InstaException(result.Info.Message);
        }
    }

    public async Task SetBio()
    {
        var result = await _instaApi.AccountProcessor.SetBiographyAsync(GetRandomMessage());
        if (!result.Succeeded)
        {
            throw new InstaException(result.Info.Message);
        }
    }

    private string GetRandomMessage()
    {
        var r = new Random().Next(_bios.Count);
        return _bios[r];
    }
}