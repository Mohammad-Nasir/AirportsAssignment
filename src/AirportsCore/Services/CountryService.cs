using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using AirportsCore.Common;
using AirportsCore.Contracts;
using AirportsCore.Models;
using System.Linq;

namespace AirportsCore.Services
{
    public class CountryService : ICountryService
    {
        private Country[] _countries;
        /// <summary>
        /// GetCountriesAsync
        /// </summary>
        /// <returns></returns>
        public async virtual Task<Country[]> GetCountriesAsync()
        {

            var root = AppDomain.CurrentDomain.BaseDirectory;
            var dataPath = Constants.COUNTRIES_FEED;
            dataPath = dataPath.Replace("~/", root);

            var result = File.ReadAllText(dataPath);
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var serializer = new DataContractJsonSerializer(typeof(Country[]));
            _countries = (Country[])serializer.ReadObject(ms);
            return _countries != null
                ? _countries.Where(country => country.Region.Equals("Europe"))
                    .Select(location => location).ToArray()
                : _countries;
        }
    }
}