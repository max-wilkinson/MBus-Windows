using Newtonsoft.Json;
using System.Collections.Generic;

namespace MBusBackend.Models
{
    public class Eta
    {
        [JsonProperty(PropertyName = "bus_id")]
        public int? BusId { get; set; }

        [JsonProperty(PropertyName = "route")]
        public int RouteId { get; set; }

        [JsonProperty(PropertyName = "avg")]
        public int MinutesAway { get; set; }

        public string RouteName { get; set; }
    }

    /// <summary>
    /// Contains all of the Eta's for a given route + stop combo
    /// </summary>
    public class CombinedEta : Eta
    {
        public List<int> MinutesAwayCollection { get; set; }

        public CombinedEta(Eta eta)
        {
            BusId = eta.BusId;
            RouteId = eta.RouteId;
            RouteName = eta.RouteName;

            MinutesAwayCollection = new List<int>();
            AddEta(eta.MinutesAway);
        }

        internal void AddEta(int minutesAway)
        {
            MinutesAwayCollection.Add(minutesAway);
        }
    }
}
