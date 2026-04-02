using System.Net;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Service;
using Helper.DataReader;
using TestCaseSources;

namespace Tests.UserTest;
public class UserTest : BaseTest
{
    [Test]
    public async Task GetUserDetailSuccessfully()
    {
        var response = await UserService.GetUserDetail(UserId, Token);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var schemaJson = DataReader.ReadRaw("User/UserSchema.json");
        JSchema schema = JSchema.Parse(schemaJson);
        JObject contentActualResult = JObject.Parse(response.Content!);

        bool isValid = contentActualResult.IsValid(schema);
        Assert.That(isValid, Is.True);
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
