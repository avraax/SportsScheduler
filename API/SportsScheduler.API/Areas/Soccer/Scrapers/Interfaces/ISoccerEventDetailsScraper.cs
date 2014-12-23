using SportsScheduler.API.Areas.Soccer.Models;

namespace SportsScheduler.API.Areas.Soccer.Scrapers.Interfaces
{
    public interface ISoccerEventDetailsScraper
    {
        SoccerEventDetail EventDetails(string eventId);
    }
}
