using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebAPIVersioning.Custom
{
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        // Versioning using customController selectior with query string parameter
        /*public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // First fetch all the available Web API controllers
            var controllers = GetControllerMapping();
            // Get the controller name and the parameter values from the request URI
            var routeData = request.GetRouteData();
            // Get the controller name from route data.
            // The name of the controller in our case is "Employees"
            var controllerName = routeData.Values["controller"].ToString();
            // Set the Default version number to 1
            string versionNumber = "1";
            var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
            if (versionQueryString["v"] != null)
            {
                versionNumber = versionQueryString["v"];
            }
            if (versionNumber == "1")
            {
                // if the version number is 1, then append V1 to the controller name.
                // So at this point the, controller name will become EmployeesV1
                controllerName = controllerName + "V1";
            }
            else
            {
                // if version number is 2, then append V2 to the controller name.
                // So at this point the controller name will become EmployeesV2
                controllerName = controllerName + "V2";
            }
            HttpControllerDescriptor controllerDescriptor;
            if (controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            return null;
        }*/

        // Versioning using customController selectior with Custom Header
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // First fetch all the available Web API controllers
            var controllers = GetControllerMapping();
            // Get the controller name and the parameter values from the request URI
            var routeData = request.GetRouteData();
            // Get the controller name from route data.
            // The name of the controller in our case is "Employees"
            var controllerName = routeData.Values["controller"].ToString();
            // Set the Default version number to 1
            string versionNumber = "1";

            // Get the version number from Custom header
            // You can give any name to this custom header. You have to use this
            // same header to specify the version number when issuing a request
            string customHeader = "X-EmployeesService-Version";
            if (request.Headers.Contains(customHeader))
            {
                versionNumber = request.Headers.GetValues(customHeader).FirstOrDefault();
            }
            if (versionNumber == "1")
            {
                // if the version number is 1, then append V1 to the controller name.
                // So at this point the, controller name will become EmployeesV1
                controllerName = controllerName + "V1";
            }
            else
            {
                // if version number is 2, then append V2 to the controller name.
                // So at this point the controller name will become EmployeesV2
                controllerName = controllerName + "V2";
            }
            HttpControllerDescriptor controllerDescriptor;
            if (controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            return null;
        }
    }
}