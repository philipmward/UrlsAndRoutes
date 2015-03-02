using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    public class UserAgentConstraint : IRouteConstraint
    {
        #region --fields--

        private string requiredUserAgent;

        #endregion --fields--

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return httpContext.Request.UserAgent != null && httpContext.Request.UserAgent.Contains(requiredUserAgent);
        }

        #region --ctor--

        public UserAgentConstraint(string agentParam)
        {
            requiredUserAgent = agentParam;
        }

        #endregion --ctor--
    }
}