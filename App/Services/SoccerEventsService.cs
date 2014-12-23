using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsScheduler.Services
{
    public class SoccerEventsService
    {
        public async Task<IList<SoccerEvent>> Get()
        {
            try
            {
                HttpResponseMessage response;
                using (var httpClient = new HttpClient())
                {
                    response = await httpClient.GetAsync("http://192.168.1.20/soccer/events");
                }

                var json = await response.Content.ReadAsStringAsync();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<IList<SoccerEvent>>(json);
            }
            catch (Exception ex)
            {
                return new List<SoccerEvent>();
            }
        }
    }
}