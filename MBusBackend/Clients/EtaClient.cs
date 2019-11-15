using System.Collections.Generic;
using System.Threading.Tasks;
using MBusBackend.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace MBusBackend.Clients
{
    /// <summary>
    /// Fetches the list of current ETAs from the UMich servers.
    /// </summary>
    class BasicEtaClient : IEtaClient
    {
        private string url = "http://mbus.doublemap.com/map/v2/eta?stop=";

        public async Task<IEnumerable<Eta>> GetFromServerAsync(int stopId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url + stopId);
                var json = await response.Content.ReadAsStringAsync();

                var etas = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, List<Eta>>>>>(json);

                return etas["etas"][stopId.ToString()]["etas"];
            }
        }
    }
}
