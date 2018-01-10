using Desafio.Data.Repositories;
using Desafio.Domain.Interfaces;
using Desafio.Util.Helpers;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Desafio.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            config.MapHttpAttributeRoutes();

            var container = new UnityContainer();
            container.RegisterType<IProfileRepository, ProfileRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<ITelefoneRepository, TelefoneRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
