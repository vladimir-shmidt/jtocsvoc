using Newtonsoft.Json;

namespace Core
{
    public class Wind
    {
        public float Speed { get; set; }
        [JsonProperty("deg")]
        public int Degrees { get; set; }
    }
}
