using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Helpers.Interfaces
{
    public interface IHttpClientHelper
    {
        Task<TResponse> PostAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content);
    }
}
