using System.Collections.Generic;
using System.Threading.Tasks;
using MBusBackend.Models;
using MBusBackend.Clients;
using System.Linq;

namespace MBusBackend.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private IClient<Route> Client;
        private List<Route> Routes;

        private async Task UpdateRoutes()
        {
            Routes = (await Client.GetFromServerAsync()).ToList();
        }

        public RouteRepository()
        {
            Client = ClientFactory.GetRouteClient();
            Routes = new List<Route>();
        }

        public async Task<List<Route>> GetRoutesAsync()
        {
            if (Routes.Count == 0)
                await UpdateRoutes();

            return Routes;
        }

        public async Task<Route> GetRouteAsync(int routeId)
        {
            if (Routes.Count == 0)
                await UpdateRoutes();

            return Routes.Find(r => r.Id == routeId);
        }
    }
}
