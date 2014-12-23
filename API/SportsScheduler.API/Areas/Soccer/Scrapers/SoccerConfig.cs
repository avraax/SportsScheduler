using System.Collections.Generic;
using SportsScheduler.API.Areas.Soccer.Scrapers.Interfaces;

namespace SportsScheduler.API.Areas.Soccer.Scrapers
{
    public class SoccerConfig
    {
        public static IList<ISoccerEventsScraper> Scrapers()
        {
            return new List<ISoccerEventsScraper>()
               {
                   new LiveSoccerTvEventsScraper(),
                   new BoldDkEventsScraper()
               };
        }
    }
}
