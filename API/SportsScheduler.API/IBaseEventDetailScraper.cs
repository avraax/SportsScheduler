using System;
using System.Collections.Generic;
using SportsScheduler.API.Areas.Soccer.Models;

namespace SportsScheduler.API
{
    public interface IBaseEventDetailScraper
    {
        IEventDetail EventDetail();
    }

    public interface IEventDetail
    {
        string EventId { get; set; }
        DateTime StartTimeUtc { get; set; }
        IList<Channel> Channels { get; set; }
    }
}