using MBusBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MBusBackend.Clients
{
    internal interface IClient<T>
    {
        Task<IEnumerable<T>> GetFromServerAsync();
    }

    internal interface IEtaClient
    {
        Task<IEnumerable<Eta>> GetFromServerAsync(int stopId);
    }

    internal static class ClientFactory
    {
        public static IClient<Route> GetRouteClient()
        {
            return new BasicRoutesClient();
        }

        public static IClient<Stop> GetStopClient()
        {
            return new ActiveStopsClient();
        }

        public static IEtaClient GetEtaClient()
        {
            return new BasicEtaClient();
        }
    }
}
