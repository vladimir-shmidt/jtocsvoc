using System.Net.Http;
using System.Threading.Tasks;

namespace Core
{
    public class CoreHttpClient : IHttpClient
    {
        private HttpClient _client;

        public CoreHttpClient(HttpClient client)
        {
            _client = client;
        }

        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }

        public async Task<string> GetStringAsync(string requestUri)
        {
            return await _client.GetStringAsync(requestUri);
        }
    }
}
