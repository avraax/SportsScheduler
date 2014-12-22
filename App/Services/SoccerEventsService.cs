using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;

namespace SportsScheduler.Services
{
	public class SoccerEventsService
	{
		public IList<SoccerEvent> Get(){
			try
			{
				using (var client = new HttpClient ()) {
					var response = client.GetStringAsync ("http://192.168.1.20/soccer/events").ConfigureAwait (false).GetAwaiter ().GetResult ();

					if (string.IsNullOrEmpty(response))
						return null;

					return Newtonsoft.Json.JsonConvert.DeserializeObject<IList<SoccerEvent>>(response);
				}
			}
			catch(Exception ex){
				return new List<SoccerEvent> ();
			}

			return new List<SoccerEvent> ();
		}
	}
}