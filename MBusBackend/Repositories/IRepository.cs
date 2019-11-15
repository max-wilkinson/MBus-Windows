using System.Collections.Generic;
using System.Threading.Tasks;
using MBusBackend.Models;

namespace MBusBackend.Repositories
{
    public interface IRouteRepository
    {
        Task<List<Route>> GetRoutesAsync();
        Task<Route> GetRouteAsync(int routeId);
    }

    public interface IEtaRepository
    {
        Task<List<CombinedEta>> GetEtasAsync(int stopId);
    }

    public interface IStopRepository
    {
        Task<List<Stop>> GetStopsAsync();
    }

    public static class RepositoryFactory
    {
        private static RouteRepository Routes = new RouteRepository();
        private static StopRepository Stops = new StopRepository();
        private static EtaRepository Etas = new EtaRepository();

        public static IRouteRepository GetRouteRepository()
        {
            return Routes;
        }

        public static IStopRepository GetStopRepository()
        {
            return Stops;
        }

        public static IEtaRepository GetEtaRepository()
        {
            return Etas;
        }
    }
}
