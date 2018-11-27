using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using System;

namespace Core.Tests
{
    [TestFixture]
    public class JsonToCsvSerializerTests
    {
        private ISerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new JsonToCsvSerializer();
        }

        [Test]
        public void Deserialize_IntegerValue_SetPropertyWithGivenValue()
        {
            var task = _serializer.Deserialize<Stub>("{\"integer\":42}");
            task.Wait();
            Assert.AreEqual(42, task.Result.Integer);
        }

        [Test]
        public void Deserialize_UnixEpochDate_SetDatePropertyWithConvertedValue()
        {
            var task = _serializer.Deserialize<Stub>("{\"date\":1488868246}");
            task.Wait();
            Assert.AreEqual(DateTime.Parse("2017-03-07 06:30:46"), task.Result.Date);
        }
    }

    public class Stub
    {
        public int Integer { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }
    }

}
