﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Infra
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Dependency Resolution.
            config.DependencyResolver = new DependencyResolver();

            // CORS Support.
            var cors = new EnableCorsAttribute("http://localhost:9999", "*", "*");
            config.EnableCors(cors);

            // Shortened Route.
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Ping",
                routeTemplate: "",
                defaults: new { controller = "Ping", action = "Ping" }
            );

            config.Routes.MapHttpRoute(
                name: "API",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
