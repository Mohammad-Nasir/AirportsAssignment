
using System.Configuration;

namespace AirportsCore.Common
{
    public static class Constants
    {
        public static readonly string COUNTRIES_FEED = ConfigurationManager.AppSettings["COUNTRIES_FEED"];
        public static readonly string AIRPORTS_FEED = ConfigurationManager.AppSettings["AIRPORTS_FEED"];
        public static readonly string KEY_CACHE_LOCATIONS = ConfigurationManager.AppSettings["KEY_CACHE_LOCATIONS"];
        public static readonly int CACHE_TIMEOUT = int.Parse(ConfigurationManager.AppSettings["CACHE_TIMEOUT"]);
    }
}