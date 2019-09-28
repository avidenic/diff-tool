using System;
using Descartes.Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Descartes.Demo.Infrastructure
{
    /// <summary>
    /// A route constraint that checks that supplied data is indeed a side enum
    /// </summary>
    public class SideRouteConstraint : IRouteConstraint
    {
        // TODO: extend to accept enum type as a parameter
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var match = values[routeKey]?.ToString();
            if (string.IsNullOrEmpty(match))
            {
                return false;
            }
            return Enum.TryParse(match, true, out Side side);
        }
    }
}