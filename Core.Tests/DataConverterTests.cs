using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Core.Tests
{
    [TestFixture]
    public class DataConverterTests
    {
        private DataConverter _converter;
        private IWeatherForcastClient _client;
        private ISerializer _serializer;
        private IWriter _writer;


        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<IWeatherForcastClient>();
            _serializer = Substitute.For<ISerializer>();
            _writer = Substitute.For<IWriter>();
            _converter = new DataConverter(_client, _serializer, _writer);
        }

        [Test]
        public void Convert_SomeJson_TakeForecastByService()
        {
            _converter.Convert().Wait();
            _client.Received(1).GetWetherForcast();
        }

        [Test]
        public void Convert_SomeJson_DeserializeForecast()
        {
            _converter.Convert().Wait();
            _serializer.Received(1).Deserialize<Forecast>(Arg.Any<string>());
        }

        [Test]
        public void Convert_ExpectedJson_DeserializeGivenJson()
        {
            _client.GetWetherForcast().Returns("STUB JSON");
            _converter.Convert().Wait();
            _serializer.Received(1).Deserialize<Forecast>("STUB JSON");
        }

        [Test]
        public void Convert_SomeForecast_SerializeForecast()
        {
            _converter.Convert().Wait();
            _serializer.Received(1).Serialize(Arg.Any<Forecast>());
        }

        [Test]
        public void Convert_ExpectedForecast_SerializeGivenForecast()
        {
            var actual = new Forecast();
            _serializer.Deserialize<Forecast>(Arg.Any<string>()).Returns(Task.FromResult(actual));
            _converter.Convert().Wait();
            _serializer.Received(1).Serialize(actual);
        }

        [Test]
        public void Convert_SomeForecast_SerializeGivenForecast()
        {
            var actual = new Forecast();
            _serializer.Deserialize<Forecast>(Arg.Any<string>()).Returns(Task.FromResult(actual));
            _converter.Convert().Wait();
            _serializer.Received(1).Serialize(actual);
        }

        [Test]
        public void Convert_SomeCsv_WriteCsv()
        {
            _converter.Convert().Wait();
            _writer.Received(1).Write(Arg.Any<string>());
        }

        [Test]
        public void Convert_ExpectedCsv_WriteGivenCsv()
        {
            _serializer.Serialize(Arg.Any<Forecast>()).Returns("STUB CSV");
            _converter.Convert().Wait();
            _writer.Received(1).Write("STUB CSV");
        }

        [Test]
        public void Dispose_GivenClient_DisposeClient()
        {
            _converter.Dispose();
            _client.Received(1).Dispose();
        }
    }
}
