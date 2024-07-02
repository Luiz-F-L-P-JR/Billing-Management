using Billing.Management.Application.Billing.HttpRequests.DTOs;
using Billing.Management.Application.Billing.HttpRequests.Interface;
using Billing.Management.Domain.HttpHandler.Interface;
using System.Text.Json;

namespace Billing.Management.Application.Billing.HttpRequests.Service
{
    public class BillingServiceRequests : IBillingServiceRequests
    {
        private readonly IHttpRequests? _http;
        private const string? HTTP_PROTOCOL = $@"https://";
        private const string? BILLING_API_ROUTE = $@"{HTTP_PROTOCOL}65c3b12439055e7482c16bca.mockapi.io/api/v1/billing";

        public BillingServiceRequests(IHttpRequests? http)
        {
            _http = http;
        }

        public async Task<IEnumerable<BillingRequestDTO>> GetAllAsync()
        {
            var uri = BILLING_API_ROUTE;
            var request = await _http.GetAsync(uri);

            if (request.IsSuccessStatusCode)
            {
                string stringAsync = await request.Content.ReadAsStringAsync();
                var billings = JsonSerializer.Deserialize<IEnumerable<BillingRequestDTO>>
                                (
                                    stringAsync, 
                                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                                );

                return billings;
            }
            return null;
        }

        public async Task<BillingRequestDTO> GetAsync(int id)
        {
            var uri = $"{BILLING_API_ROUTE}/{id}";
            var request = await _http.GetAsync(uri);

            if (request.IsSuccessStatusCode)
            {
                string stringAsync = await request.Content.ReadAsStringAsync();
                var billing = JsonSerializer.Deserialize<BillingRequestDTO>
                                (
                                    stringAsync,
                                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                                );

                return billing;
            }
            return null;
        }

        public async Task CreateAsync(BillingRequestDTO entity)
        {
            var uri = BILLING_API_ROUTE;
            await _http.PostAsync(uri, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var uri = $"{BILLING_API_ROUTE}/{id}";
            await _http.DeleteAsync(uri);
        }

        public async Task UpdateAsync(BillingRequestDTO entity)
        {
            var uri = BILLING_API_ROUTE;
            await _http.PutAsync(uri, entity);
        }
    }
}
