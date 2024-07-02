
using System.Text.Json;
using System.Text;
using Billing.Management.Domain.HttpHandler.Interface;

namespace Billing.Management.Infra.Data.HttpHandler
{
    public class HttpRequests : IHttpRequests
    {
        private readonly HttpClient? _httpClient;

        public HttpRequests(HttpClient? httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await _httpClient?.SendAsync(httpRequestMessage);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item)
        {
            return await DoRequestAsync(HttpMethod.Post, uri, item);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item)
        {
            return await DoRequestAsync(HttpMethod.Put, uri, item);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            var response = await _httpClient?.SendAsync(httpRequestMessage);

            return response;
        }

        private async Task<HttpResponseMessage> DoRequestAsync<T>(HttpMethod httpMethod, string uri, T item)
        {
            var httpRequestMessage = new HttpRequestMessage(httpMethod, uri)
            {
                Content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient?.SendAsync(httpRequestMessage);

            return response;
        }
    }
}
