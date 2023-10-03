using Catalog.Front.Helpers.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Helpers
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpClientHelper(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TResponse> PostAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content)
        {
            var client = _clientFactory.CreateClient("catalog");

            var token = await GetAccessTokenAsync();
            if (token == null)
            {
                return default!;
            }

            if (!string.IsNullOrEmpty(token))
            {
                client.SetBearerToken(token);
            }

            var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url, stringContent);

            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(resultContent);
                if (response != null)
                {
                    return response;
                }
            }

            return default!;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var client = _clientFactory.CreateClient("identityServerClient");

            var tokenRequest = new Dictionary<string, string>
                {
                        { "client_id", "yourClientId" },
                        { "client_secret", "yourClientSecret" },
                        { "grant_type", "client_credentials" },
                };

            var requestContent = new FormUrlEncodedContent(tokenRequest);
            var response = await client.PostAsync("/connect/token", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                dynamic responseData = JObject.Parse(jsonResult);
                string accessToken = responseData.access_token;
                return accessToken;
            }

            return default!;
        }
    }
}
