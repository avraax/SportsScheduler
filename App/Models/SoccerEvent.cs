using System;

namespace SportsScheduler
{
	public class SoccerEvent
	{
		public string Referrer { get; set; }
		public string EventId { get; set; }
		public string Title { get; set; }
		public Uri Url { get; set; }
		public DateTime StartTimeUtc { get; set; }
	}
}