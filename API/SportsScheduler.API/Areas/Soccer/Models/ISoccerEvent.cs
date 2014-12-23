using System;

namespace SportsScheduler.API.Areas.Soccer.Models
{
    public interface ISoccerEvent : IEvent
    {
        string Referrer { get; set; }
        string Title { get; set; }
        Uri Url { get; set; }
    }
}