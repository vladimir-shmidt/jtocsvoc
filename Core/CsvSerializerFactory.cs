using System;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Core
{
    public class CsvSerializerFactory
    {
        public CsvWriter Get(TextWriter writer)
        {
            var csv = new CsvWriter(writer);
            csv.Configuration.RegisterClassMap<ForecastClassMap>();
            return csv;
        }

        public sealed class ForecastClassMap : ClassMap<Forecast>
        {
            public ForecastClassMap()
            {
                Map(m => m.Date).Index(0).TypeConverter<DateTimeConverter>();
                Map(m => m.System.Sunrise).Index(1).TypeConverter<DateTimeConverter>();
                Map(m => m.System.Sunset).Index(2).TypeConverter<DateTimeConverter>();
                Map(m => m.Temperature.Minimum).Index(3);
                Map(m => m.Temperature.Maximum).Index(4);
                Map(m => m.Temperature.Pressure).Index(5);
                Map(m => m.Temperature.Humidity).Index(6);
                Map(m => m.Wind.Speed).Index(7);
            }

            private class DateTimeConverter : ITypeConverter
            {
                public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
                {
                    throw new NotImplementedException();
                }

                public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
                {
                    var date = value as DateTime?;
                    if (date == null || !date.HasValue) return string.Empty;
                    var utc = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);
                    var zone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                    var british = TimeZoneInfo.ConvertTime(utc, zone);
                    return british.ToLocalTime().ToString("HH:mm:ss");
                }
            }
        }
    }
}
