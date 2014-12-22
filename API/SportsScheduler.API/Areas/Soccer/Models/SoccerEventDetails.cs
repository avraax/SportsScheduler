using System;
using System.Collections.Generic;

namespace SportsScheduler.API.Areas.Soccer.Models
{
    public class SoccerEventDetails
    {
        public string EventId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public IList<Channel> Channels { get; set; }
    }
}
