using System;
using System.Collections.Generic;
using SportsScheduler.API.Areas.Soccer.Models;

namespace SportsScheduler.API
{
    public interface IBaseEventsScraper
    {
        List<ISoccerEvent> Scrape();
    }

    public interface IEvent
    {
        string EventId { get; set; }
        DateTime StartTimeUtc { get; set; }
    }
}