using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using AirportsCore.Common;
using AirportsCore.Contracts;
using AirportsCore.Models;

namespace AirportsTests.MockServices
{
    public class MockLocationService : ILocationService
    {
        private Location[] _locations;

        /// <summary>
        /// GetLocationsAsync
        /// </summary>
        /// <returns></returns>
        public async Task<Location[]> GetLocationsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ExtractLoctions
        /// </summary>
        /// <returns></returns>
        public async Task<Location[]> ExtractLocations()
        {
            throw new NotImplementedException();
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