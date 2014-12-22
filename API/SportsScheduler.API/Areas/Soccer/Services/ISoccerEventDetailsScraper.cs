using SportsScheduler.API.Areas.Soccer.Models;

namespace SportsScheduler.API.Areas.Soccer.Services
{
    public interface ISoccerEventDetailsScraper
    {
        SoccerEventDetails EventDetails(string eventId);
    }
}
