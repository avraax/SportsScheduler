using System;
using System.Collections.Generic;

namespace SportsScheduler.API.Areas.Soccer.Models
{
    public class SoccerEvent
    {
        public string EventId { get; set; }
        public DateTime StartTimeUtc { get; set; }

        public string Source { get; set; }
        public string Title { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public Uri Url { get; set; }
        public IList<Channel> Channels { get; set; }
    }
}