using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using MBusBackend.Models;

namespace MBus
{
    public class LocationService
    {
        public async Task<IEnumerable<Stop>> DetermineNearbyStops(IEnumerable<Stop> activeStops)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    var location = await GetUserLocation();
                    return FilterStopsByDistance(location, activeStops);
                case GeolocationAccessStatus.Denied:
                    return activeStops;
                case GeolocationAccessStatus.Unspecified:
                    return activeStops;
                default:
                    return activeStops;
            }
        }

        private async Task<Geocoordinate> GetUserLocation()
        {
            var geolocator = new Geolocator { DesiredAccuracyInMeters = 15 };
            Geoposition position = await geolocator.GetGeopositionAsync();

            return position.Coordinate;
        }

        private IEnumerable<Stop> FilterStopsByDistance(Geocoordinate location, IEnumerable<Stop> stops, int stopsToTake = 10)
        {
            var stopsByDistance = new SortedList<double, Stop>();

            foreach (var stop in stops)
            {
                var distance = CalculateDistance(stop, location);
                stopsByDistance.Add(distance, stop);
            }

            return stopsByDistance.Values.Take(stopsToTake);
        }

        private double CalculateDistance(Stop stop, Geocoordinate location)
        {
            var latitude = location.Point.Position.Latitude;
            var longitude = location.Point.Position.Longitude;

            return Math.Sqrt((Math.Pow(stop.Latitude - latitude, 2)) + (Math.Pow(stop.Longitude - longitude, 2)));
        }
    }
}
