using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    public class LegacyRoute : RouteBase
    {
        #region --fields--

        private readonly string[] urls;

        #endregion --fields--

        #region --public methods--

        #region --RouteBase Members--

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var requestedURL = httpContext.Request.AppRelativeCurrentExecutionFilePath;
            if (!urls.Contains(requestedURL, StringComparer.OrdinalIgnoreCase)) return null;

            var result = new RouteData(this, new MvcRouteHandler());
            result.Values.Add("controller", "Legacy");
            result.Values.Add("action", "GetLegacyURL");
            result.Values.Add("legacyURL", requestedURL);
            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;

            if (values.ContainsKey("legacyURL") &&
                urls.Contains((string)values["legacyURL"], StringComparer.OrdinalIgnoreCase))
            {
                result = new VirtualPathData(this, new UrlHelper(requestContext).Content((string)values["legacyURL"]).Substring(1));
            }
            return result;
        }

        #endregion --RouteBase Members--

        #endregion --public methods--

        #region --ctor--

        public LegacyRoute(params string[] targetUrls)
        {
            urls = targetUrls;
        }

        #endregion --ctor--
    }
}