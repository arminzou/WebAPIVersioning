﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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
        }*/

        // Versioning using customController selectior with Accept Header
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

            // Get the version number from the Accept header
            // Users can include multiple Accept headers in the request
            // Check if any of the Accept headers has a parameter with name version
            var acceptHeader = request.Headers.Accept.Where(a => a.Parameters
                                .Count(p => p.Name.ToLower() == "version") > 0);
            // If there is atleast one header with a "version" parameter
            if (acceptHeader.Any())
            {
                // Get the version parameter value from the Accept header
                versionNumber = acceptHeader.First().Parameters
                                .First(p => p.Name.ToLower() == "version").Value;
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

        // Versioning using customController selectior with Custom Media Types
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

            // Get the version number from the Custom media type
            // We need to use the regular expression for mataching the pattern of the 
            // media type. We have given a name for the matched group that contains
            // the version number which enables us to retrieve the version number 
            // using the group name("version") instead of ZERO based index
            string regex = @"application\/vnd\.dotnettutorials\.([a-z]+)\.v(?<version>[0-9]+)\+([a-z]+)";
            // Users can include multiple Accept headers in the request.
            // So we need to check atlest if any of the Accept headers has our custom 
            // media type by checking if there is a match with regular expression specified
            var acceptHeader = request.Headers.Accept
                .Where(a => Regex.IsMatch(a.MediaType, regex, RegexOptions.IgnoreCase));
            // If there is atleast one Accept header with our custom media type
            if (acceptHeader.Any())
            {
                // Retrieve the first custom media type
                var match = Regex.Match(acceptHeader.First().MediaType, regex, RegexOptions.IgnoreCase);
                // From the version group, get the version number
                versionNumber = match.Groups["version"].Value;
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