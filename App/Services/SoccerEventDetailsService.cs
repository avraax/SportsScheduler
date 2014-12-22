using System;
using System.Net.Http;

namespace SportsScheduler.Services
{
	public class SoccerEventDetailsService
	{
		public SoccerEventDetails Get(string eventId){
			try
			{
				using (var client = new HttpClient ()) {
					var url = string.Format("http://192.168.1.20/soccer/eventdetails/{0}", eventId);
					var response = client.GetStringAsync (url).ConfigureAwait (false).GetAwaiter ().GetResult ();

					if (string.IsNullOrEmpty(response))
						return null;

					return Newtonsoft.Json.JsonConvert.DeserializeObject<SoccerEventDetails>(response);
				}
			}
			catch(Exception ex){
				return null;
			}

			return null;
		}
	}
}

