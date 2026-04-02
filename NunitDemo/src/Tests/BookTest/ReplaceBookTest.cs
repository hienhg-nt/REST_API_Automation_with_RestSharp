using System.Net;
using FluentAssertions;
using Helper.DataReader;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Service;
using TestCaseSources;

namespace Tests.BookTest;
public class ReplaceBookTest : BaseTest
{
    private string? _isbn;

    [SetUp]
    public async Task Setup()
    {
        var result = await BookService.AddBookSuccessfullyAndGetIsbn("9781449325862", UserId, Token);
        _isbn = result.isbn;
    }

    [TestCase("9781449331818")]
    public async Task ReplaceABookSuccessfullyWithValidData(string isbn)
    {
        var body = new DeleteAndReplaceBookModel { userId = UserId, isbn = isbn };
        var response = await BookService.ReplaceBook(
            body: body!,
            isbn: _isbn!,
            token: Token
        );

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var schemaJson = DataReader.ReadRaw("Book/ReplaceBookSchema.json");
        JSchema schema = JSchema.Parse(schemaJson);
        JObject contentActualResult = JObject.Parse(response.Content!);

        bool isValid = contentActualResult.IsValid(schema);
        Assert.That(isValid, Is.True);
    }

    [Test]
    [TestCaseSource(
        typeof(BookTestCases),
        nameof(BookTestCases.ReplaceABookUnsuccessfullyWithInvalidData))]
    public async Task ReplaceABookUnsuccessfullyWithInvalidData(string token, string userId, string oldIsbn, string newIsbn)
    {
        userId = (userId == "default") ? UserId : userId;
        token = (token == "default") ? Token : token;
        var body = new DeleteAndReplaceBookModel { userId = userId, isbn = newIsbn };
        var response = await BookService.ReplaceBook(
            body: body!,
            token: token,
            isbn: oldIsbn
        );

        response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.Unauthorized);
    }

    [TearDown]
    public async Task Teardown()
    {
        var body = new DeleteAndReplaceBookModel{isbn = _isbn, userId = UserId};
        await BookService.DeleteBookFromCollection(body: body, token: Token);
    }
}
