using System.Threading.Tasks;
using AirportsCore.Models;

namespace AirportsCore.Contracts
{
    public interface ICountryService
    {
        Task<Country[]> GetCountriesAsync();
    }
}