using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Core
{
    public class Forecast
    {
        [JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }
        [JsonProperty("weather")]
        public IEnumerable<Weather> Weathers { get; set; }
        public string Base { get; set; }
        [JsonProperty("main")]
        public Temperature Temperature { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        [JsonProperty("dt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }
        [JsonProperty("sys")]
        public System System { get; set; }
        public int Id { get; set; }
        [JsonProperty("name")]
        public string City { get; set; }
        [JsonProperty("cod")]
        public int Code { get; set; }
    }
}
