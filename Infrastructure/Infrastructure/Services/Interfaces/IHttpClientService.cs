namespace Infrastructure.Services.Interfaces;

public interface IHttpClientService
{
    Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content);
    Task<TResponse> SendAsync<TResponse>(string url, HttpMethod method);
    Task SendAsync(string url, HttpMethod method);
    Task SendAsync<TRequest>(string url, HttpMethod method, TRequest? content);
}