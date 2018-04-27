using System;
using AirportsCore.Models;

namespace AirportsCore.Contracts
{
    public interface IDistanceCalculator
    {
        double Calculate(Coordinates sourceCoordinates, Coordinates destinationCoordinates);
    }
}