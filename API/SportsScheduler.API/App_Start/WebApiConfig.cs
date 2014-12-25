using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Practices.Unity;
using SportsScheduler.API.App_Start;
using SportsScheduler.API.Areas.Soccer.Scrapers;
using SportsScheduler.API.Areas.Soccer.Scrapers.Interfaces;

namespace SportsScheduler.API
{
    public static class WebApiConfig
    {
        private static IUnityContainer _container;

        public static void Register(HttpConfiguration config)
        {
            SetupContainer();
            config.DependencyResolver = new UnityResolver(_container);
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void SetupContainer()
        {
            _container = new UnityContainer();

            _container.RegisterType<ISoccerEventsScraper, LiveSoccerTvEventsScraper>("LiveSoccerTvEventsScraper");
            _container.RegisterType<LiveSoccerEventsConfig>().RegisterInstance(LiveSoccerEventsConfig.FromConfig());

            _container.RegisterType<ISoccerEventsScraper, BoldDkEventsScraper>("BoldDkEventsScraper");
            _container.RegisterType<BoldDkEventsConfig>().RegisterInstance(BoldDkEventsConfig.FromConfig());

            _container.RegisterType<IEnumerable<ISoccerEventsScraper>, ISoccerEventsScraper[]>();
        }
    }
}
