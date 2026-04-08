using System.Collections;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using Service;

namespace Service;
public class BookService : BaseService
{
    public BookService()
    {
        BasePath = "/BookStore/v1";
    }

    public async Task<RestResponse> AddBookToCollection(object body, string token, string? userId = null, string? isbn = null)
    {
        var headers = AddHeaders(accept: true, contentType: true, token: token);
        if (userId is not null && body is not null)
        {
            var userIdProp = body.GetType().GetProperty("userId");
            if (userIdProp != null && userIdProp.CanWrite) userIdProp.SetValue(body, userId);
        }
        if (isbn is not null && body is not null)
        {
            var prop = body.GetType().GetProperty("collectionOfIsbns");
            var list = prop?.GetValue(body) as IList;
            list?[0]?.GetType().GetProperty("isbn")?.SetValue(list[0], isbn);
        }
        RestRequest request = CreateRequest("/Books", Method.Post, headers, body);
        return await Client.ExecuteAsync(request);
    }

    public async Task<RestResponse> DeleteBookFromCollection(object body, string token)
    {
        var headers = AddHeaders(accept: true, contentType: true, token: token);
        RestRequest request = CreateRequest("/Book", Method.Delete, headers, body);
        return await Client.ExecuteAsync(request);
    }

    public async Task<RestResponse> ReplaceBook(object body, string isbn, string token)
    {
        var headers = AddHeaders(accept: true, contentType: true, token: token);
        RestRequest request = CreateRequest($"/Books/{isbn}", Method.Put, headers, body);
        return await Client.ExecuteAsync(request);
    }

    public async Task<RestResponse> GetAllBook<T>()
    {
        var headers = AddHeaders(accept: true);
        RestRequest request = CreateRequest($"/Books", Method.Get, headers);
        return await Client.ExecuteAsync(request);
    }

    public async Task<(string isbn, HttpStatusCode statusCode)> AddBookSuccessfullyAndGetIsbn(string isbn, string userId, string token)
    {
        var body = new AddBooksRequestModel{
            userId = userId,
            collectionOfIsbns = new List<CollectionOfIsbnsModel> 
            {
                new CollectionOfIsbnsModel { isbn = isbn }
            }
        };
        
        var response = await AddBookToCollection(
            body: body!,
            token: token
        );

        if (string.IsNullOrEmpty(response.Content)) return (string.Empty, response.StatusCode);
        var content = response.Content;
        var json = JObject.Parse(content!);
        var books = json["books"] as JArray;
        var actualIsbn = books![0]["isbn"]!.ToString();
        return (actualIsbn, response.StatusCode);
    }
}
