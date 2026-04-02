using System.Net;
using FluentAssertions;
using Service;
using TestCaseSources;

namespace Tests.BookTest;
public class DeleteBookTest : BaseTest
{
    private string? _isbn;

    [SetUp]
    public async Task Setup(){
        var result = await BookService.AddBookSuccessfullyAndGetIsbn("9781449325862", UserId, Token);
        _isbn = result.isbn;
    }

    [Test]
    public async Task DeleteABookSuccessfullyWithValidData()
    {
        var body = new DeleteAndReplaceBookModel{isbn = _isbn, userId = UserId};
        var response = await BookService.DeleteBookFromCollection(
            body: body!,
            token: Token
        );

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        response.Content.Should().BeNullOrEmpty();
    }

    [Test]
    [TestCaseSource(
        typeof(BookTestCases),
        nameof(BookTestCases.DeleteABookUnuccessfullyWithInvalidData))]
    public async Task DeleteABookUnuccessfullyWithInvalidData(string token, string userId, string isbn)
    {
        userId = (userId.ToLower() == "default") ? UserId : userId;
        token = (token.ToLower() == "default") ? Token : token;
        var body = new DeleteAndReplaceBookModel{isbn = isbn, userId = userId};
        var response = await BookService.DeleteBookFromCollection(
            body: body!,
            token: token
        );

        response.StatusCode.Should().BeOneOf(HttpStatusCode.Unauthorized, HttpStatusCode.BadRequest);
    }

    [TearDown]
    public async Task Teardown()
    {
        var body = new DeleteAndReplaceBookModel{isbn = _isbn, userId = UserId};
        await BookService.DeleteBookFromCollection(body: body, token: Token);
    }

}
