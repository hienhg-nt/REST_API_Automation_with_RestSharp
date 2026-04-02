using System.Net;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using RestSharp;
using Service;
using Helper.DataReader;
using TestCaseSources;

namespace Tests.BookTest;
public class AddBookTest : BaseTest
{
    private string? _isbn;

    [TestCase("9781449325862")]
    public async Task AddABookSuccessfullyWithValidData(string isbn)
    {
        var result = await BookService.AddBookSuccessfullyAndGetIsbn(isbn, UserId, Token);
        result.statusCode.Should().Be(HttpStatusCode.Created);
        _isbn = result.isbn;
        Assert.That(_isbn, Is.EqualTo(isbn));
    }

    [Test]
    [TestCaseSource(
        typeof(BookTestCases),
        nameof(BookTestCases.AddABookUnsuccessfullyWithInvalidData))]
    public async Task AddABookUnsuccessfullyWithInvalidData(string token, string userId, string isbn)
    {
        var body = DataReader.Data<UserModel>("Book/AddBookData.json", "AddBookWithInvalidData");
        userId = (userId.ToLower() == "default") ? UserId : userId;
        token = (token.ToLower() == "default") ? Token : token;
        var response = await BookService.AddBookToCollection(body: body!, token: token, userId: userId, isbn: isbn);
        response.StatusCode.Should().BeOneOf(HttpStatusCode.Unauthorized, HttpStatusCode.BadRequest);
    }

    [TestCase("9781449325862")]
    public async Task AddABookUnsuccessfullyWithAddedBook(string isbn)
    {
        var addedBook = await BookService.AddBookSuccessfullyAndGetIsbn(isbn, UserId, Token);
        var body = DataReader.Data<UserModel>("Book/AddBookData.json", "AddBookWithInvalidData");
        var response = await BookService.AddBookToCollection(body: body!, token: Token, userId: UserId, isbn: addedBook.isbn);
        response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest);
    }

    [TearDown]
    public async Task Teardown()
    {
        var body = new DeleteAndReplaceBookModel{isbn = _isbn, userId = UserId};
        await BookService.DeleteBookFromCollection(body: body, token: Token);
    }
}
