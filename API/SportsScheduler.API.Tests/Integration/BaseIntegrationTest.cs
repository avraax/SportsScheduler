using System;
using Microsoft.Practices.Unity;
using SportsScheduler.API.Areas.Soccer.Scrapers;
using SportsScheduler.API.Areas.Soccer.Scrapers.Interfaces;

namespace SportsScheduler.API.Tests.Integration
{
    public class BaseIntegrationTest : IDisposable
    {
        public static IUnityContainer Container;

        public BaseIntegrationTest()
        {
            Container = new UnityContainer();

            //var redisServer = ConfigurationManager.AppSettings["RedisServer"];
            //Container.RegisterInstance<IRedisClientsManager>(new PooledRedisClientManager(1000, 10, redisServer));

            Container.RegisterType<ISoccerEventsScraper, LiveSoccerTvEventsScraper>();
            Container.RegisterType<ISoccerEventDetailsScraper, LiveSoccerTvEventDetailsScraper>();
        }

        public void Dispose()
        {
            
        }
    }
}
