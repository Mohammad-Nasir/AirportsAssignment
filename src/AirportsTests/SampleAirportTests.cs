using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AirportsCore.Services;
using AirportsCore.Models;
using AirportsTests.MockServices;
using System;

namespace AirportsTests
{
    [TestClass]
    public class SampleAirportTests
    {
        [TestMethod]
        public void TEST_FILTER_LOCATIONS_RETURN_AIRPORTS()
        {
            var expected = 1;
            var locations = new Location[]
            {
                new Location()
                {
                    iata=  "BIU",
                    lon=  "-23.983334",
                    iso=  "IS",
                    status=  1,
                    name=  "Bildudalur Airport",
                    continent=  "EU",
                    type=  "airport",
                    lat=  "65.833336",
                    size=  "small"
                },
                new Location()
                {
                    iata = "BJD",
                    lon = "-14.75",
                    iso = "IS",
                    status = 1,
                    name = null,
                    continent = "EU",
                    type = "airport",
                    lat = "66.066666",
                    size = "small"
                }
            };
            var service = new MockLocationService();

            var result = service.FilterLocations(locations);

            Assert.AreEqual(expected, result.Length);
        }

        [TestMethod]
        public void TEST_CALCULATE_DISTANCE_THROWS_EXCEPTION_WHEN_COORDINATES_ARE_NULL()
        {
            Exception expectedExp = null;
            var service = new DistanceCalculator();

            try
            {
                service.Calculate(null, null);
            }
            catch (AssertFailedException ex)
            {
                expectedExp = ex;
            }

            Assert.IsNotNull(expectedExp);
        }

        [TestMethod]
        public void TEST_CALCULATE_DISTANCE_RETURNS_DISTANCE()
        {
            var expected = 0;
            var service = new DistanceCalculator();
            var source = new Coordinates
            {
                latitude = 10
                ,
                longitude = 10
            };

            var destination = new Coordinates
            {
                latitude = 10
                ,
                longitude = 10
            };

            var actual = service.Calculate(source, destination);

            Assert.AreEqual(expected, actual);
        }
    }
}
