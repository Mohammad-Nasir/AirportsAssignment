using System.Threading.Tasks;
using AirportsCore.Models;

namespace AirportsCore.Contracts
{
    public interface ILocationService
    {
        Task<Location[]> GetLocationsAsync();
    }
}