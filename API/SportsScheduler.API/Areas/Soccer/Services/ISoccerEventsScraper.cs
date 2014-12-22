using System.Collections.Generic;
using SportsScheduler.API.Areas.Soccer.Models;

namespace SportsScheduler.API.Areas.Soccer.Services
{
    public interface ISoccerEventsScraper
    {
        IList<SoccerEvent> GetSoccerEvents();
    }
}
