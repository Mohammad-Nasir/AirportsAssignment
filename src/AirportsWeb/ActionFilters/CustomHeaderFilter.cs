using System.Web.Mvc;
using AirportsCore.Common;
using AirportsCore.Services;
using AirportsCore.Contracts;

namespace AirportsWeb.ActionFilters
{
    public class CustomHeaderFilter : ActionFilterAttribute
    {
        private readonly ICachingManager _cachingManager;
        public CustomHeaderFilter()
            :this(DependencyResolver.Current.GetService<ICachingManager>())
        {
        }

        public CustomHeaderFilter(ICachingManager cachineManager)
        {
            _cachingManager = cachineManager;
        }

        private string _customHeaderValue;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _customHeaderValue = _cachingManager.GetFromCache(Constants.KEY_CACHE_LOCATIONS) == null ? "true" : "false";
            filterContext.HttpContext.Response.Headers.Add("from-feed", _customHeaderValue);
        }
    }
}