using Newtonsoft.Json;

namespace Core
{
    public class Temperature
    {
        public float Temp { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        [JsonProperty("temp_max")]
        public float Maximum { get; set; }
        [JsonProperty("temp_min")]
        public float Minimum { get; set; }
    }
}
