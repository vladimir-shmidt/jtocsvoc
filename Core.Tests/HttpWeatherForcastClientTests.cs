using NSubstitute;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace Core.Tests
{
    [TestFixture]
    public class HttpWeatherForcastClientTests
    {
        private IHttpClient _http;
        private IWeatherForcastClient _client;

        [SetUp]
        public void Setup()
        {
            _http = Substitute.For<IHttpClient>();
            _client = new HttpWeatherForcastClient("API", 3, _http);
        }

        [Test]
        public void GetWetherForcast_GivenUrl_DownloadFromGivenUrl()
        {
            _client.GetWetherForcast().Wait();
            _http.Received(1).GetStringAsync("API");
        }

        [Test]
        public void GetWetherForcast_SomeData_ReturnServiceResultAsGiven()
        {
            _http.GetStringAsync(Arg.Any<string>()).Returns("RESULT");
            var task =_client.GetWetherForcast();
            task.Wait();
            Assert.AreEqual("RESULT", task.Result);
        }

        [Test]
        public void GetWetherForcast_ServerFailedOnce_CallServiceSecondTime()
        {
            _http.GetStringAsync("API").Returns(x => { throw new HttpRequestException(); }, x => "RESULT");
            _client.GetWetherForcast().Wait();
            _http.Received(2).GetStringAsync("API");
        }

        [Test]
        public void GetWetherForcast_ServerFailedTwice_CallServiceThirdTime()
        {
            _http.GetStringAsync("API").Returns(x => { throw new HttpRequestException(); }, x => { throw new HttpRequestException(); }, x => "RESULT");
            _client.GetWetherForcast().Wait();
            _http.Received(3).GetStringAsync("API");
        }

        [Test]
        public void GetWetherForcast_ServerFailedMoreThenThreeTimes_DoNotCallServiesMore()
        {
            _http.GetStringAsync("API").Returns(x => { throw new HttpRequestException(); }, x => { throw new HttpRequestException(); }, x => { throw new HttpRequestException(); }, x => { throw new HttpRequestException(); }, x => "RESULT");
            try { _client.GetWetherForcast().Wait(); } catch (Exception ex) { }
            _http.Received(3).GetStringAsync("API");
        }

        [Test]
        public void GetWetherForcast_ServerFailedMoreThenThreeTimes_ReturnAggregatedException()
        {
            _http.GetStringAsync("API").Returns(x => { throw new HttpRequestException(); }, x => { throw new HttpRequestException(); }, x => { throw new HttpRequestException(); }, x => { throw new HttpRequestException(); }, x => "RESULT");
            Assert.CatchAsync<DataConverterException>(_client.GetWetherForcast);
        }
    }
}
