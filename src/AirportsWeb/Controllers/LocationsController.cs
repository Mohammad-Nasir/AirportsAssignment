using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AirportsCore.Common;
using AirportsCore.Contracts;
using AirportsCore.Models;
using AirportsWeb.ActionFilters;

namespace AirportsWeb.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILocationService _service;
        private readonly ICountryService _countryService;
        private readonly IDistanceCalculator _distanceCalculator;
        private readonly ICachingManager _cachingManager;

        public LocationsController(ILocationService service
            , ICountryService countryService
            , IDistanceCalculator distanceCalculator
            , ICachingManager cachingManager)
        {
            _service = service;
            _countryService = countryService;
            _distanceCalculator = distanceCalculator;
            _cachingManager = cachingManager;
        }

        // GET: Locations
        [OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 300, VaryByParam = "none")]
        public async Task<JsonResult> Countries()
        {
            var countries = await _countryService.GetCountriesAsync();
            var result = Json(countries, JsonRequestBehavior.AllowGet);
            return result;
        }

        [CustomHeaderFilter]
        //[OutputCache(Duration = 300, VaryByParam = "none")]
        public async Task<JsonResult> Airports()
        {
            var locations = await _service.GetLocationsAsync();
            return Json(locations, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Distance()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetDistance(DistanceInput distanceInput)
        {
            double distance = 0;
            var airportsData = _cachingManager.GetFromCache(Constants.KEY_CACHE_LOCATIONS) as Location[];

            if (airportsData == null)
                return Json(distance, JsonRequestBehavior.AllowGet);

            var source = airportsData.First(airport => airport.iata.Equals(distanceInput.SourceAirport));
            var destination = airportsData.First(airport => airport.iata.Equals(distanceInput.DestinationAirport));

            var sourceCoordinates = new Coordinates();
            var destinationCoordinates = new Coordinates();
            if (source != null && destination != null)
            {
                sourceCoordinates = new Coordinates
                {
                    latitude = double.Parse(source.lat),
                    longitude = double.Parse(source.lon)
                };
                destinationCoordinates = new Coordinates
                {
                    latitude = double.Parse(destination.lat),
                    longitude = double.Parse(destination.lon)
                };
            }

            distance = _distanceCalculator.Calculate(sourceCoordinates, destinationCoordinates);

            return Json(distance, JsonRequestBehavior.AllowGet);
        }
    }
}
