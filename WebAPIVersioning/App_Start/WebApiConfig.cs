using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

            //Adding the custom media type to the JsonFormatter
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("application/vnd.dotnettutorials.employees.v1+json"));
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("application/vnd.dotnettutorials.employees.v2+json"));
            //Adding the custom media type to the XmlFormatter
            config.Formatters.XmlFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("application/vnd.dotnettutorials.employees.v1+xml"));
            config.Formatters.XmlFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("application/vnd.dotnettutorials.employees.v2+xml"));
        }
    }
}
