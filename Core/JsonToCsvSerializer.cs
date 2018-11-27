using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class JsonToCsvSerializer : ISerializer
    {
        private Newtonsoft.Json.JsonSerializer _deserializer;
        private CsvSerializerFactory _factory;

        public JsonToCsvSerializer() : this(Newtonsoft.Json.JsonSerializer.Create(), new CsvSerializerFactory()) { }

        public JsonToCsvSerializer(Newtonsoft.Json.JsonSerializer deserializer, CsvSerializerFactory factory)
        {
            _deserializer = deserializer;
            _factory = factory;
        }

        public async Task<T> Deserialize<T>(string obj)
        {
            using(var reader = new Newtonsoft.Json.JsonTextReader(new StringReader(obj)))
            {
                return  await Task.FromResult<T>(_deserializer.Deserialize<T>(reader));
            }
        }

        public async Task<string> Serialize<T>(T obj)
        {
            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
            {
                using (var csv = _factory.Get(writer))
                {
                    csv.WriteHeader<Forecast>();
                    await csv.NextRecordAsync();
                    csv.WriteRecord(obj);
                    await csv.FlushAsync();
                    return builder.ToString();
                }
            }
        }
    }
}
