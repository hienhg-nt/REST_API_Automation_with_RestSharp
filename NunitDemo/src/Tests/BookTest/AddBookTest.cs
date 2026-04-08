using System.Net;
using FluentAssertions;
using Service;
using Helper.DataReader;
using TestCaseSources;

namespace Tests.BookTest;
public class AddBookTest : BaseTest
{
    private string? _isbn;

    [Test]
    [TestCaseSource(
        typeof(BookTestCases),
        nameof(BookTestCases.AddBookSuccessfully))]
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
        var body = DataReader.Data<AddBooksRequestModel>("Book/AddBookData.json", "AddBookWithInvalidData");
        userId = (userId.ToLower() == "default") ? UserId : userId;
        token = (token.ToLower() == "default") ? Token : token;
        var response = await BookService.AddBookToCollection(body: body!, token: token, userId: userId, isbn: isbn);
        response.StatusCode.Should().BeOneOf(HttpStatusCode.Unauthorized, HttpStatusCode.BadRequest);
    }

    [Test]
    [TestCaseSource(
        typeof(BookTestCases),
        nameof(BookTestCases.AddABookUnsuccessfullyWithAddedBook))]
    public async Task AddABookUnsuccessfullyWithAddedBook(string isbn)
    {
        var addedBook = await BookService.AddBookSuccessfullyAndGetIsbn(isbn, UserId, Token);
        var body = DataReader.Data<AddBooksRequestModel>("Book/AddBookData.json", "AddBookWithInvalidData");
        var response = await BookService.AddBookToCollection(body: body!, token: Token, userId: UserId, isbn: addedBook.isbn);
        response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest);
    }

    [TearDown]
    public async Task Teardown()
    {
        if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(_isbn))
        {
            var body = new DeleteAndReplaceBookModel{isbn = _isbn, userId = UserId};
            await BookService.DeleteBookFromCollection(body: body, token: Token);        
        }
    }
}
