using Newtonsoft.Json;

namespace MBusBackend.Models
{
    /// <summary>
    /// Contains the properties representing a bus stop.
    /// Example stop: Pierpont Commons
    /// </summary>
    public class Stop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public double Longitude { get; set; }
    }
}
