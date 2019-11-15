using System.Collections.Generic;
using Newtonsoft.Json;

namespace MBusBackend.Models
{
    /// <summary>
    /// Contains the properties representing a route.
    /// Example route: Commuter North
    /// </summary>
    public class Route
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        [JsonProperty(PropertyName = "start_time")]
        public string StartTime { get; set; }

        [JsonProperty(PropertyName = "end_time")]
        public string EndTime { get; set; }

        public bool Active { get; set; }

        public List<int> Stops { get; set; }
    }
}
