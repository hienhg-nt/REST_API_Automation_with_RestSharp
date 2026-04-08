using System.Net;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Service;
using TestCaseSources;

namespace Tests.UserTest;
public class UserTest : BaseTest
{
    [Test]
    public async Task GetUserDetailSuccessfully()
    {
        var response = await UserService.GetUserDetail(UserId, Token);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = JObject.Parse(response.Content!);
        json["userId"]!.ToString().Should().Be(UserId);
        json["username"]!.ToString().Should().NotBeNullOrEmpty();
        json["books"].Should().NotBeNull();
    }

    [Test]
    [TestCaseSource(
        typeof(UserTestCases),
        nameof(UserTestCases.GetUserDetailUnsuccessfullyTestCases))]
    public async Task GetUserDetailUnsuccessfully(string userId, string token)
    {
        var actualUserId = userId.ToLower() == "default" ? UserId : userId;
        var actualToken = token.ToLower() == "default" ? Token : token;

        var response = await UserService.GetUserDetail(actualUserId, actualToken);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
