using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportsScheduler.API.Areas.Soccer.Providers;

namespace SportsScheduler.API.Areas.Soccer.Controllers
{
    public class SoccerEventsController : ApiController
    {
        private readonly MultipleSoccerScraperProvider _soccerScraperProvider;

        public SoccerEventsController(MultipleSoccerScraperProvider soccerScraperProvider)
        {
            _soccerScraperProvider = soccerScraperProvider;
        }

        [HttpGet]
        [Route("soccer/events")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _soccerScraperProvider.GetEvents());
        }
    }
}