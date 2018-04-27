using System;
using System.Web;
using System.Web.Caching;
using AirportsCore.Common;
using AirportsCore.Contracts;

namespace AirportsCore.Services
{
    public class CachingManager : ICachingManager
    {
        /// <summary>
        /// AddToCache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void AddToCache(string key, object data)
        {
            HttpContext.Current.Cache.Add(key
                , data
                , null
                , Cache.NoAbsoluteExpiration
                , new TimeSpan(0, Constants.CACHE_TIMEOUT, 0)
                , CacheItemPriority.Normal
                , null);
        }

        /// <summary>
        /// GetFromCache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetFromCache(string key)
        {
            return HttpContext.Current.Cache.Get(key);
        }
    }
}