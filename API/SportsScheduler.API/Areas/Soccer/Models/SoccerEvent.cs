using System;

namespace SportsScheduler.API.Areas.Soccer.Models
{
    public class SoccerEvent : ISoccerEvent
    {
        public string EventId { get; set; }
        public DateTime StartTimeUtc { get; set; }

        public string Referrer { get; set; }
        public string Title { get; set; }
        public Uri Url { get; set; }
    }
}