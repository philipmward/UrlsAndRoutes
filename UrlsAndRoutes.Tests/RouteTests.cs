using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace UrlsAndRoutes.Tests
{
    [TestClass]
    public class RouteTests
    {
        #region --test methods--

        //[TestMethod]
        //public void TestIncomingRoutes()
        //{
        //    ////check for the URL that is hoped for, ~ is at the start because that is how ASP.NET framework presents the URL to the routing system
        //    //TestRouteMatch("~/Admin/Index", "Admin", "Index");
        //    ////check that the values are being obtained from the segments
        //    //TestRouteMatch("~/One/Two", "One", "Two");
        //    //TestRouteMatch("~/", "Home", "Index");
        //    //TestRouteMatch("~/Customer", "Customer", "Index");
        //    //TestRouteMatch("~/Shop/Index", "Home", "Index");

        //    ////ensure that too many or too few segments fails to match - this normally wouldn't fail in production, but because of the code commented out of the RouteConfig.RegisterRotes method, it will only match 2 segments or less.
        //    //TestRouteFail("~/Admin/Index/Segment");

        //    //Above was all pre-changes

        //    //TestRouteMatch("~/", "Home", "Index");
        //    //TestRouteMatch("~/Customer", "Customer", "Index");
        //    //TestRouteMatch("~/Customer/List", "Customer", "List");
        //    //TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "All" });
        //    //TestRouteMatch("~/Customer/List/All/Delete", "Customer", "List", new { id = "All", catchall = "Delete" });
        //    //TestRouteMatch("~/Customer/List/All/Delete/Perm", "Customer", "List",
        //    //    new { id = "All", catchall = "Delete/Perm" }); //catch all keeps all extra segments together. I'm required to break them apart for use.
        //    //TestRouteFail("~/Customer/List/All/Delete");

        //    //and more changes
        //    TestRouteMatch("~/", "Home", "Index");
        //    TestRouteMatch("~/Home", "Home", "Index");
        //    TestRouteMatch("~/Home/Index", "Home", "Index");

        //    TestRouteMatch("~/Home/About", "Home", "About");
        //    TestRouteMatch("~/Home/About/MyId", "Home", "About", new { id = "MyId" });
        //    TestRouteMatch("~/Home/About/MyId/More/Segments", "Home", "About", new { id = "MyId", catchall = "More/Segments" });

        //    TestRouteFail("~/Home/OtherAction");
        //    TestRouteFail("~/Account/Index");
        //    TestRouteFail("~/Account/About");
        //}

        #endregion --test methods--

        #region --private methods--

        private static HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            //create mock request
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(p => p.AppRelativeCurrentExecutionFilePath)
                .Returns(targetUrl);
            mockRequest.Setup(p => p.HttpMethod)
                .Returns(httpMethod);

            //create mock response
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(p => p.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns<string>(s => s);

            //create the mock context
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(p => p.Request)
                .Returns(mockRequest.Object);
            mockContext.Setup(p => p.Response)
                .Returns(mockResponse.Object);

            //return mocked context
            return mockContext.Object;
        }

        private static bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;

            var result = valCompare(routeResult.Values["controller"], controller) &&
                          valCompare(routeResult.Values["action"], action);

            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                //if there are any that are not in the route reslut and values match swap result to false
                if (propInfo.Any(pi => !(routeResult.Values.ContainsKey(pi.Name)
                                         && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null)))))
                {
                    result = false;
                }
            }
            return result;
        }

        private static void TestRouteFail(string url)
        {
            //Arrange
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            //Assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        private static void TestRouteMatch(string url, string controller, string action, object routeProperties = null,
            string httpMethod = "GET")
        {
            //arrange
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        #endregion --private methods--
    }
}