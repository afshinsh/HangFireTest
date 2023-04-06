using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HangFireTest.Services.Utility
{
    public class HttpService
    {
        private readonly IDictionary<string, string> _headers;

        public HttpService()
        {
            _headers = new Dictionary<string, string>();
        }

        private RestRequest CreateRequest(Method method)
        {
            var request = new RestRequest { Method = method };
            foreach (var header in _headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            return request;

        }

        public Task<string> GetAsync(string url)
        {
            var request = CreateRequest(Method.Get);
            return ExecuteAsync(url, request);
        }

        public Task<string> PostAsync(string url, object body)
        {
            var request = CreateRequest(Method.Post);
            request.AddJsonBody(body);
            return ExecuteAsync(url, request);
        }

        public async Task<T> GetAsync<T>(string url)
        {
            return ExtractData<T>(await GetAsync(url));
        }

        public async Task<T> PostAsync<T>(string url, object body)
        {
            return ExtractData<T>(await PostAsync(url, body));
        }

        private static async Task<string> ExecuteAsync(string url, RestRequest request)
        {
            var client = new RestClient(url);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        private T ExtractData<T>(string response)
        {
            try
            {
                return response.FromJson<T>();
            }
            catch
            {
                return default;
            }
        }

        public void AddHeader(string key, string value)
        {
            _headers[key] = value;
        }

        public void Authorization(string value)
        {
            _headers[nameof(Authorization)] = value;
        }
    }
}
