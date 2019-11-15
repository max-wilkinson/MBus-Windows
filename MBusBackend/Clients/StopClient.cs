using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MBusBackend.Models;
using MBusBackend.Repositories;

namespace MBusBackend.Clients
{
    /// <summary>
    /// Fetches bus stops that are currently in use.
    /// </summary>
    class ActiveStopsClient : IClient<Stop>
    {
        private string url = "http://mbus.doublemap.com/map/v2/stops";
        private IRouteRepository routeRepository = RepositoryFactory.GetRouteRepository();

        public async Task<IEnumerable<Stop>> GetFromServerAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var routes = await routeRepository.GetRoutesAsync();

                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var stops = JsonConvert.DeserializeObject<IEnumerable<Stop>>(json);

                return FilterStops(stops, routes);
            }
        }

        private IEnumerable<Stop> FilterStops(IEnumerable<Stop> stops, List<Route> routes)
        {
            IEnumerable<int> activeStops = Enumerable.Empty<int>();
            routes.ForEach(route => activeStops = activeStops.Union(route.Stops));

            var activeStopSet = new HashSet<int>(activeStops);
            stops = stops.Where(s => activeStopSet.Contains(s.Id));

            return stops;
        }
    }
}
