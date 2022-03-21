using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaxService.Models;

namespace TaxService.Services
{
    public class TaxCalculator : ITaxCalculator
    {
        private static JsonSerializerOptions JSON_SERIALIZER_OPTIONS => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private static string RATES = "rates";
        private static string TAXES = "taxes";
        private readonly TaxCalculatorOptions taxCalculatorOptions;
        private readonly HttpClient httpClient;

        public TaxCalculator(IOptions<TaxCalculatorOptions> options, HttpClient httpClient)
        {
            this.taxCalculatorOptions = options.Value;
            httpClient.BaseAddress = new Uri(this.taxCalculatorOptions.ApiEndpoint);
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Token token=\"{this.taxCalculatorOptions.ApiKey}\"");
            this.httpClient = httpClient;
        }

        public async Task<RateResult> GetTaxRates(string zipcode, string city, string country)
        {
            if (string.IsNullOrEmpty(zipcode))
                throw new ArgumentException("Invalid zipcode");

            var requestUrl = $"{RATES}/{zipcode}?city={city}&country={country}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            using (var httpResponse = await httpClient.SendAsync(httpRequest))
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                RateResult rates = JsonSerializer.Deserialize<RateResult>(content, JSON_SERIALIZER_OPTIONS);
                return rates;
            }
        }

        public async Task<TaxResult> GetTaxes(Order order)
        {
            if (order == null)
                throw new ArgumentException("Order is required");

            var jsonPayload = JsonSerializer.Serialize(order);
            var payload = new StringContent(jsonPayload, Encoding.UTF8, MediaTypeNames.Application.Json);

            using (var httpResponse = await httpClient.PostAsync(TAXES, payload))
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                TaxResult taxResult = JsonSerializer.Deserialize<TaxResult>(content, JSON_SERIALIZER_OPTIONS);
                return taxResult;
            }
        }
    }
}
