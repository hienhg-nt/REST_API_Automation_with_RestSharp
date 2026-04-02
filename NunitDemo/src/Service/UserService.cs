using RestSharp;
using Helper.DataReader;


namespace Service;
public class UserService : BaseService
{
    public UserService()
    {
        BasePath = "/Account/v1";
    }

    public async Task<RestResponse> PostUser<T>(string funct, string data, string key)
    {
        var body = DataReader.Data<T>(data, key);
        var headers = AddHeaders(accept: true, contentType: true);
        string endpoint = funct switch
        {
            "account" => "/User",
            "token" => "/GenerateToken",
            "authorize" => "/Authorized",
            _ => throw new ArgumentException($"Invalid function type: {funct}")  
        };

        RestRequest request = CreateRequest(endpoint, Method.Post, headers, body);
        return await Client.ExecuteAsync(request);
    }

    public async Task<RestResponse> CreateNewAccount<T>(string data, string key) => await PostUser<T>("account", data, key);
    public async Task<RestResponse> AuthorizedAccount<T>(string data, string key) => await PostUser<T>("authorize", data, key);
    public async Task<RestResponse> GenerateTokenForAccount<T>(string data, string key) => await PostUser<T>("token", data, key);

    public async Task<RestResponse> DeleteUser<T>(string userId, string token)
    {
        var headers = AddHeaders(accept: true, token: token);
        RestRequest request = CreateRequest($"/User/{userId}", Method.Delete, headers);
        return await Client.ExecuteAsync(request);
    }

    public async Task<RestResponse> GetUserDetail(string userId, string token)
    {
        var headers = AddHeaders(accept: true, token: token); 
        RestRequest request = CreateRequest($"/User/{userId}", Method.Get, headers);
        return await Client.ExecuteAsync(request);
    }
}
