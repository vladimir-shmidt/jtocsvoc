using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Core
{
    public class System
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public float Message { get; set; }
        public string Country { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Sunrise { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Sunset { get; set; }
    }
}
