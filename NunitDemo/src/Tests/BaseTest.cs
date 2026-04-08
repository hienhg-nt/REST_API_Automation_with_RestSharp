using Newtonsoft.Json.Linq;
using Core.Api;
using Service;
using Helper.Configuration;

namespace Tests;
public abstract class BaseTest
{
    protected UserService UserService;
    protected BookService BookService;
    protected string UserId = string.Empty;
    protected string Token = string.Empty;

    [OneTimeSetUp]
    public async Task GlobalSetup()   
    {
        var config = ConfigurationHelper.ReadConfiguration("appsetting.json");
        string testUrl = ConfigurationHelper.GetConfigurationValue(config, "TestUrl");
        ApiClient.Start(testUrl);
        UserService = new UserService();
        BookService = new BookService();

        var response = await UserService.CreateNewAccount<CreateUserRequestModel>("User/UserData.json", "ValidUser");
        var content = response.Content;
        var json = JObject.Parse(content!);
        UserId = json["userID"]!.ToString();

        response = await UserService.GenerateTokenForAccount<CreateUserRequestModel>("User/UserData.json", "ValidUser");
        content = response.Content;
        json = JObject.Parse(content!);
        Token = json["token"]!.ToString();
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(Token))
        {
            await UserService.DeleteUser<GetUserResponseModel>(UserId, Token);
        }
    }
}
