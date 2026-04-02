using RestSharp;
using Core.Api;

namespace Service
{
    public abstract class BaseService
    {
        protected readonly RestClient Client;
        protected string BasePath { get; set; } = "";
        
        public BaseService()
        {
            Client = ApiClient.client;
        }

        protected RestRequest CreateRequest(
            string endpoint,
            Method method,
            Dictionary<string, string>? headers = null,
            object? body = null
        )
        {
            var request = new RestRequest($"{BasePath}{endpoint}", method);
            if (headers != null)
            {
                foreach (var header in headers) request.AddHeader(header.Key, header.Value);
            }
            if (body != null) request.AddJsonBody(body);
            return request;
        }

        protected Dictionary<string, string> AddHeaders(bool accept = false, bool contentType = false, string? token = null)
        {
            var headers = new Dictionary<string, string>();
            if (accept) headers["accept"] = "application/json";
            if (contentType) headers["Content-Type"] = "application/json";
            if (!string.IsNullOrEmpty(token)) headers["Authorization"] = $"Bearer {token}";
            return headers;
        }

    }
}
