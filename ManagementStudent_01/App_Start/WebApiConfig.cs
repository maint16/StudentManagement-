﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ApiMultiPartFormData;

namespace ManagementStudent_01
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Add(new ApiMultipartFormDataFormatter());
        }
    }
}
