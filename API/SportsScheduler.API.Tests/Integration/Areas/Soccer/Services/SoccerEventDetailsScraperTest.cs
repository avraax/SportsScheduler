using Microsoft.Practices.Unity;
using SportsScheduler.API.Areas.Soccer.Scrapers.Interfaces;
using Xunit;

namespace SportsScheduler.API.Tests.Integration.Areas.Soccer.Services
{
    public class SoccerEventDetailsScraperTest : BaseIntegrationTest
    {
        private readonly ISoccerEventDetailsScraper _scraper;

        public SoccerEventDetailsScraperTest()
        {
            _scraper = Container.Resolve<ISoccerEventDetailsScraper>();
        }

        [Fact]
        public void GetSoccerEventDetails()
        {
            var events = _scraper.EventDetails("1207165");

            Assert.NotNull(events);
        }
    }
}
