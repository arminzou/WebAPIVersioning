using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using WebAPIVersioning.Custom;

namespace WebAPIVersioning
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            // Versioning with URI using convention-based routing
            /*config.Routes.MapHttpRoute(
                name: "Version1",
                routeTemplate: "api/v1/employees/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "EmployeesV1" }
            );

            config.Routes.MapHttpRoute(
                name: "Version2",
                routeTemplate: "api/v2/employees/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "EmployeesV2" }
            );*/

            // Versioning with Query String Parameter 
            config.Services.Replace(typeof(IHttpControllerSelector),
                               new CustomControllerSelector(config));

            config.Routes.MapHttpRoute(
                name: "DefaultRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
