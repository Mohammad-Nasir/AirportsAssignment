using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using AirportsCore.Common;
using AirportsCore.Contracts;
using AirportsCore.Models;

namespace AirportsCore.Services
{

    public class LocationService : ILocationService
    {
        private Location[] _locations;
        private readonly ICachingManager _cachingManager;

        public LocationService(ICachingManager cachingManager)
        {
            _cachingManager = cachingManager;
        }

        /// <summary>
        /// GetLocationsAsync
        /// </summary>
        /// <returns></returns>
        public async Task<Location[]> GetLocationsAsync()
        {
            if (_cachingManager.GetFromCache(Constants.KEY_CACHE_LOCATIONS) != null)
                _locations = _cachingManager.GetFromCache(Constants.KEY_CACHE_LOCATIONS) as Location[];
            else
            {
                _locations = await ExtractLocations();
                _cachingManager.AddToCache(Constants.KEY_CACHE_LOCATIONS, _locations);
            }

            return _locations;
        }

        /// <summary>
        /// ExtractLoctions
        /// </summary>
        /// <returns></returns>
        public async Task<Location[]> ExtractLocations()
        {
            var http = new HttpClient();
            var response = await http.GetAsync(Constants.AIRPORTS_FEED);
            var jsonString = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Location[]));

            var locationsStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            _locations = (Location[])serializer.ReadObject(locationsStream);

            _locations = FilterLocations(_locations);
            return _locations;
        }

        public Location[] FilterLocations(Location[] locations)
        {
            return locations != null
                ? locations.Where(location => location.continent == "EU")
                    .Where(location => location.type == "airport")
                    .Where(location => !string.IsNullOrEmpty(location.name))
                    .Select(location => location).ToArray()
                : locations;
        }
    }
}