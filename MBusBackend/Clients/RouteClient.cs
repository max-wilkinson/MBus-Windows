using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MBusBackend.Models;

namespace MBusBackend.Clients
{
    /// <summary>
    /// Fetches the list of routes and returns the list unmodified.
    /// </summary>
    class BasicRoutesClient : IClient<Route>
    {
        private string url = "http://mbus.doublemap.com/map/v2/routes";

        public async Task<IEnumerable<Route>> GetFromServerAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var routes = JsonConvert.DeserializeObject<List<Route>>(json);

                return routes;
            }
        }
    }
}
