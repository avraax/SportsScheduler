using System.Collections.Generic;
using SportsScheduler.API.Areas.Soccer.Models;

namespace SportsScheduler.API.Areas.Soccer.Scrapers.Interfaces
{
    public interface ISoccerEventsScraper
    {
        List<SoccerEvent> Scrape();
        bool Enabled { get; }
    }
}
