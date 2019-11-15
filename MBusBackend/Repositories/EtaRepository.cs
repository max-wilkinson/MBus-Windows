using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBusBackend.Models;
using MBusBackend.Clients;
using MBusBackend.Utilities;

namespace MBusBackend.Repositories
{
    public class EtaRepository : IEtaRepository
    {
        #region Properties
        private IEtaClient Client;
        private IRouteRepository RouteRepository;
        private ITimerUtility Timer;
        private Dictionary<int, List<CombinedEta>> Etas;
        private Dictionary<int, long> TimeLastUpdated;
        #endregion

        #region Constructors
        public EtaRepository()
        {
            Client = ClientFactory.GetEtaClient();
            RouteRepository = RepositoryFactory.GetRouteRepository();
            Timer = TimerUtilityFactory.GetTimer();
            TimeLastUpdated = new Dictionary<int, long>();
            Etas = new Dictionary<int, List<CombinedEta>>();
        }
        #endregion

        #region Public Methods
        public async Task<List<CombinedEta>> GetEtasAsync(int stopId)
        {
            if (!Etas.ContainsKey(stopId) || Timer.IntervalHasPassed(TimeLastUpdated[stopId], Timer.GetCurrentTime(), 60))
                await UpdateEtas(stopId);

            return Etas[stopId];
        }
        #endregion

        #region Private Methods
        private async Task UpdateEtas(int stopId)
        {
            var etas = await Client.GetFromServerAsync(stopId);
            var etasByRoute = new Dictionary<int, CombinedEta>();

            foreach(var eta in etas)
            {
                if(etasByRoute.ContainsKey(eta.RouteId))
                    etasByRoute[eta.RouteId].AddEta(eta.MinutesAway);

                else
                {
                    eta.RouteName = (await RouteRepository.GetRouteAsync(eta.RouteId)).Name;
                    etasByRoute[eta.RouteId] = new CombinedEta(eta);
                }
            }

            Etas[stopId] = etasByRoute.Values.ToList();

            TimeLastUpdated[stopId] = Timer.GetCurrentTime();
        }
        #endregion
    }
}
