using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportsScheduler.API.Areas.Soccer.Services;

namespace SportsScheduler.API.Areas.Soccer.Controllers
{
    public class EventsController : ApiController
    {
        private readonly ISoccerEventsScraper _soccerEventsScraper;

        public EventsController(ISoccerEventsScraper soccerEventsScraper)
        {
            _soccerEventsScraper = soccerEventsScraper;
        }

        [HttpGet]
        [Route("soccer/events")]
        public HttpResponseMessage Index()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _soccerEventsScraper.GetSoccerEvents());
        }
    }
}