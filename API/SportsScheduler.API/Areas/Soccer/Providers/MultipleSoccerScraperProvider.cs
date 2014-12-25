using System.Collections.Generic;
using System.Linq;
using SportsScheduler.API.Areas.Soccer.Models;
using SportsScheduler.API.Areas.Soccer.Scrapers.Interfaces;

namespace SportsScheduler.API.Areas.Soccer.Providers
{
    public class MultipleSoccerScraperProvider
    {
        private readonly IEnumerable<ISoccerEventsScraper> _scrapers;
            
        public MultipleSoccerScraperProvider(IEnumerable<ISoccerEventsScraper> scrapers)
        {
            _scrapers = scrapers;
        }

        public List<SoccerEvent> GetEvents()
        {
            var soccerEvents = new List<SoccerEvent>();

            foreach (var scraper in _scrapers.Where(scraper => scraper.Enabled))
            {
                soccerEvents.AddRange(scraper.Scrape());
            }

            return soccerEvents;
        }
    }
}
