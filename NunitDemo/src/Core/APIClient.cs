
using RestSharp;

namespace Core.Api;
public static class ApiClient
{
    public static RestClient client { get; private set; } = null!;

    public static void Start(string url)
    {
        client = new RestClient(url);
    }
}