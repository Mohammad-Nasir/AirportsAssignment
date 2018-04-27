using System.Device.Location;
using AirportsCore.Contracts;
using AirportsCore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportsCore.Services
{
    public class DistanceCalculator : IDistanceCalculator
    {
        /// <summary>
        /// Calculate
        /// </summary>
        /// <param name="sourceCoordinates"></param>
        /// <param name="destinationCoordinates"></param>
        /// <returns></returns>
        public double Calculate(Coordinates sourceCoordinates, Coordinates destinationCoordinates)
        {
            Assert.IsNotNull(sourceCoordinates);
            Assert.IsNotNull(destinationCoordinates);

            var source = new GeoCoordinate(sourceCoordinates.latitude, sourceCoordinates.longitude);
            var destination = new GeoCoordinate(destinationCoordinates.latitude, destinationCoordinates.longitude);

            return source.GetDistanceTo(destination);
        }
    }
}