using System.Threading.Tasks;

namespace Core
{
    public class DataConverter : IConverter
    {
        private IWeatherForcastClient _client;
        private ISerializer _serializer;
        private IWriter _writer;

        public DataConverter(IWeatherForcastClient client, ISerializer serializer, IWriter writer)
        {
            _client = client;
            _serializer = serializer;
            _writer = writer;
        }

        /// <summary>
        /// Convert JSON data from API to CSV format with business rules
        /// </summary>
        /// <exception cref="Core.AggregateConnectionErrorException">Thrown when connectivity problems occurred three times in a row</exception>
        /// <returns>Converted CSV data</returns>
        public async Task Convert()
        {
            string response = await _client.GetWetherForcast();
            Forecast forecast = await _serializer.Deserialize<Forecast>(response);
            var csv = await _serializer.Serialize(forecast);
            await _writer.Write(csv);
        }

        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }
    }
}
