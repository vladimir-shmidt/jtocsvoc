using CsvHelper;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Tests
{
    [TestFixture]
    public class CsvSerializerFactoryTests
    {
        private CsvSerializerFactory _factory;
        private StringBuilder _writer;
        private CsvWriter _csv;

        [SetUp]
        public void Setup()
        {
            _writer = new StringBuilder();
            _factory = new CsvSerializerFactory();
            _csv = _factory.Get(new StringWriter(_writer));
        }

        [Test]
        public void Get_StubWriter_InitializeCsvWithHeaders()
        {
            _csv.WriteHeader<Forecast>();
            _csv.Flush();
            Assert.AreEqual("Date,Sunrise,Sunset,Minimum,Maximum,Pressure,Humidity,Speed", _writer.ToString());
        }

        [Test]
        public void Get_EmptyForecast_WriteEmptyValues()
        {
            _csv.WriteRecord(new Forecast() { });
            _csv.Flush();
            Assert.AreEqual("03:00:00,03:00:00,03:00:00,0,0,0,0,0", _writer.ToString());
        }

        [Test]
        public void Get_EndOfDayDateTime_WriteUKLOcalDateWithTimeFormat()
        {
            _csv.WriteRecord(new Forecast() { Date = new DateTime(2018, 11, 21, 23, 50, 10, DateTimeKind.Utc) });
            _csv.Flush();
            Assert.AreEqual("02:50:10,03:00:00,03:00:00,0,0,0,0,0", _writer.ToString());
        }

        [Test]
        public void Get_EndOfDaySunrise_WriteUKLOcalDateWithTimeFormat()
        {
            _csv.WriteRecord(new Forecast() { System = new System { Sunrise = new DateTime(2018, 11, 21, 23, 50, 10, DateTimeKind.Utc) } });
            _csv.Flush();
            Assert.AreEqual("03:00:00,02:50:10,03:00:00,0,0,0,0,0", _writer.ToString());
        }

        [Test]
        public void Get_EndOfDaySunset_WriteUKLOcalDateWithTimeFormat()
        {
            _csv.WriteRecord(new Forecast() { System = new System { Sunset = new DateTime(2018, 11, 21, 23, 50, 10, DateTimeKind.Utc) } });
            _csv.Flush();
            Assert.AreEqual("03:00:00,03:00:00,02:50:10,0,0,0,0,0", _writer.ToString());
        }

        [Test]
        public void Get_MinimumTemperature_WriteMinimumTemperature()
        {
            _csv.WriteRecord(new Forecast() { Temperature = new Temperature { Minimum = -100 } });
            _csv.Flush();
            Assert.AreEqual("03:00:00,03:00:00,03:00:00,-100,0,0,0,0", _writer.ToString());
        }

        [Test]
        public void Get_MaximumTemperature_WriteMaximumTemperature()
        {
            _csv.WriteRecord(new Forecast() { Temperature = new Temperature { Maximum = 100 } });
            _csv.Flush();
            Assert.AreEqual("03:00:00,03:00:00,03:00:00,0,100,0,0,0", _writer.ToString());
        }

        [Test]
        public void Get_Pressure_WritePressure()
        {
            _csv.WriteRecord(new Forecast() { Temperature = new Temperature { Pressure = 2500 } });
            _csv.Flush();
            Assert.AreEqual("03:00:00,03:00:00,03:00:00,0,0,2500,0,0", _writer.ToString());
        }

        [Test]
        public void Get_Humidity_WriteHumidity()
        {
            _csv.WriteRecord(new Forecast() { Temperature = new Temperature { Humidity = 2500 } });
            _csv.Flush();
            Assert.AreEqual("03:00:00,03:00:00,03:00:00,0,0,0,2500,0", _writer.ToString());
        }

        [Test]
        public void Get_Speed_WriteSpeed()
        {
            _csv.WriteRecord(new Forecast() { Wind = new Wind { Speed = 120} });
            _csv.Flush();
            Assert.AreEqual("03:00:00,03:00:00,03:00:00,0,0,0,0,120", _writer.ToString());
        }
    }
}
