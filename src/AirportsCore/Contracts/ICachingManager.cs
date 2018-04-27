
namespace AirportsCore.Contracts
{
    public interface ICachingManager
    {
        void AddToCache(string key, object data);
        object GetFromCache(string key);
    }
}