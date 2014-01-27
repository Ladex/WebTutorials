using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TwitterBootstrapDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
            name: "Articles",
            routeTemplate: "api/Article/GetAll/{categoryId}",
                defaults: new
                {
                    controller = "Article",
                    action = "GetAll",
                    categoryId = 1
                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
