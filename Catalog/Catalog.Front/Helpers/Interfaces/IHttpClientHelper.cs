using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Helpers.Interfaces
{
    public interface IHttpClientHelper
    {
        Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content);
        Task<TResponse> SendAsync<TResponse>(string url, HttpMethod method);
        Task SendAsync(string url, HttpMethod method);
        Task SendAsync<TRequest>(string url, HttpMethod method, TRequest? content);
    }
}
