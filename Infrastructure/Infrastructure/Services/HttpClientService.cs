using System.Text;
using IdentityModel.Client;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MVC.Services;

public class HttpClientService : IHttpClientService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _clientFactory = clientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content)
    {
        var client = _clientFactory.CreateClient();
        if (_httpContextAccessor.HttpContext == null)
        {
            return default!;
        }

        var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        if (token == null)
        {
            return default !;
        }

        if (!string.IsNullOrEmpty(token))
        {
            client.SetBearerToken(token);
        }

        var httpMessage = new HttpRequestMessage();
        httpMessage.RequestUri = new Uri(url);
        httpMessage.Method = method;

        if (content != null)
        {
            httpMessage.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

        var result = await client.SendAsync(httpMessage);

        if (result.IsSuccessStatusCode)
        {
            var resultContent = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TResponse>(resultContent);
            if (response != null)
            {
                return response;
            }
        }

        return default !;
    }

    public async Task<TResponse> SendAsync<TResponse>(string url, HttpMethod method)
    {
        var client = _clientFactory.CreateClient();
        if (_httpContextAccessor.HttpContext == null)
        {
            return default!;
        }

        var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        if (!string.IsNullOrEmpty(token))
        {
            client.SetBearerToken(token);
        }

        var httpMessage = new HttpRequestMessage();
        httpMessage.RequestUri = new Uri(url);
        httpMessage.Method = method;

        var result = await client.SendAsync(httpMessage);

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

    public async Task SendAsync<TRequest>(string url, HttpMethod method, TRequest? content)
    {
        var client = _clientFactory.CreateClient();
        if (_httpContextAccessor.HttpContext == null)
        {
            return;
        }

        var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        if (!string.IsNullOrEmpty(token))
        {
            client.SetBearerToken(token);
        }

        var httpMessage = new HttpRequestMessage();
        httpMessage.RequestUri = new Uri(url);
        httpMessage.Method = method;

        if (content != null)
        {
            httpMessage.Content =
                new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

        var result = await client.SendAsync(httpMessage);
    }

    public async Task SendAsync(string url, HttpMethod method)
    {
        var client = _clientFactory.CreateClient();
        if (_httpContextAccessor.HttpContext == null)
        {
            return;
        }

        var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        if (!string.IsNullOrEmpty(token))
        {
            client.SetBearerToken(token);
        }

        var httpMessage = new HttpRequestMessage();
        httpMessage.RequestUri = new Uri(url);
        httpMessage.Method = method;
        await client.SendAsync(httpMessage);
    }
}