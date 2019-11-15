using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBusBackend.Models;
using MBusBackend.Clients;

namespace MBusBackend.Repositories
{
    public class StopRepository : IStopRepository
    {
        private IClient<Stop> Client;
        private List<Stop> Stops;

        private async Task UpdateStops()
        {
            Stops = (await Client.GetFromServerAsync()).ToList();
        }

        public StopRepository()
        {
            Client = ClientFactory.GetStopClient();
            Stops = new List<Stop>();
        }

        public async Task<List<Stop>> GetStopsAsync()
        {
            if (Stops.Count == 0)
                await UpdateStops();

            return Stops;
        }
    }
}
