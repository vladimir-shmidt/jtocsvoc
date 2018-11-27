using System;
using System.Threading.Tasks;

namespace Core
{
    public interface IHttpClient : IDisposable
    {
        Task<string> GetStringAsync(string requestUri);
    }
}
