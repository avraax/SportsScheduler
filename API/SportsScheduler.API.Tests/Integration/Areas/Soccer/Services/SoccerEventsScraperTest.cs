using Microsoft.Practices.Unity;
using SportsScheduler.API.Areas.Soccer.Scrapers.Interfaces;
using Xunit;

namespace SportsScheduler.API.Tests.Integration.Areas.Soccer.Services
{
    public class SoccerEventsScraperTest : BaseIntegrationTest
    {
        private readonly ISoccerEventsScraper _scraper;

        public SoccerEventsScraperTest()
        {
            _scraper = Container.Resolve<ISoccerEventsScraper>();
        }

        [Fact]
        public void GetSoccerEvents()
        {
            var events = _scraper.Scrape();

            Assert.NotNull(events);
            Assert.InRange(events.Count, 1, int.MaxValue);
        }
    }
}
