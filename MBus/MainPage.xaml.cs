using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using MBusBackend.Repositories;
using MBusBackend.Models;

namespace MBus
{
    /// <summary>
    /// The Sole Page of this App
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<InfoCard> Cards;
        private LocationService LocationService = new LocationService();
        private LiveTileService TileService = new LiveTileService();
        private IStopRepository StopRepository = RepositoryFactory.GetStopRepository();
        private IEtaRepository EtaRepository = RepositoryFactory.GetEtaRepository();

        public MainPage()
        {
            this.InitializeComponent();

            Cards = new ObservableCollection<InfoCard>();
        }
        
        private async void Page_Loading(FrameworkElement sender, object args)
        {
            await GetStops();
            TileService.UpdateLiveTile(Cards[0]);
        }

        private async void UpdateLocation_Click(object sender, RoutedEventArgs e)
        {
            await GetStops();
            TileService.UpdateLiveTile(Cards[0]);
        }

        private async void UpdateTimes_Click(object sender, RoutedEventArgs e)
        {
            await UpdateTimes();
            TileService.UpdateLiveTile(Cards[0]);
        }

        private async Task UpdateTimes()
        {
            foreach (var card in Cards)
            {
                var etas = await EtaRepository.GetEtasAsync(card.Stop.Id);

                card.Etas.Clear();
                foreach (var eta in etas)
                {
                    card.Etas.Add(eta);
                }
            }
        }

        private async Task GetStops()
        {
            var activeStops = await StopRepository.GetStopsAsync();
            var nearbyStops = await LocationService.DetermineNearbyStops(activeStops);

            Cards.Clear();
            foreach(var stop in nearbyStops)
            {
                var card = new InfoCard(stop, await EtaRepository.GetEtasAsync(stop.Id));
                Cards.Add(card);
            }
        }
    }
}
