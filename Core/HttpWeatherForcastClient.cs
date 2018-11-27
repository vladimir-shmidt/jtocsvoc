using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core
{
    public class HttpWeatherForcastClient : IWeatherForcastClient
    {
        private string _api;
        private int _retry;
        private IHttpClient _client;

        public HttpWeatherForcastClient(string api, int retry, IHttpClient client)
        {
            _api = api;
            _retry = retry;
            _client = client;
        }

        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }

        /// <summary>
        /// Try to download data from API retry times.
        /// </summary>
        /// <exception cref="Core.AggregateConnectionErrorException">Thrown when connectivity problems occurred three times in a row</exception>
        /// <returns>Data downloaded from API</returns>
        public async Task<string> GetWetherForcast()
        {
            var errors = new List<ConnectionErrorException>();
            var count = _retry;
            while (count > 0)
            {
                try
                {
                    return await _client.GetStringAsync(_api);
                }
                catch (HttpRequestException ex)
                {
                    errors.Add(new ConnectionErrorException($"Failed to download data from {_api} {count} time(s)", ex));
                    count--;
                }
            }
            throw new DataConverterException($"Could not download data after {_retry} retry(s)", new AggregateConnectionErrorException(errors));
        }
    }
}
