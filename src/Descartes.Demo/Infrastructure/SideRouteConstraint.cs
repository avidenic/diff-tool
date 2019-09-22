using System;
using Descartes.Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Descartes.Demo.Infrastructure
{
    public class SideRouteConstraint : IRouteConstraint
    {
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