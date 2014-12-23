using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportsScheduler.API.Areas.Soccer.Scrapers.Interfaces;

namespace SportsScheduler.API.Areas.Soccer.Controllers
{
    public class SoccerEventDetailsController : ApiController
    {
        private readonly ISoccerEventDetailsScraper _soccerEventDetailsScraper;

        public SoccerEventDetailsController(ISoccerEventDetailsScraper soccerEventDetailsScraper)
        {
            _soccerEventDetailsScraper = soccerEventDetailsScraper;
        }

        [HttpGet]
        [Route("soccer/eventdetails/{eventid}")]
        public HttpResponseMessage Index(string eventId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _soccerEventDetailsScraper.EventDetails(eventId));
        }
    }
}
