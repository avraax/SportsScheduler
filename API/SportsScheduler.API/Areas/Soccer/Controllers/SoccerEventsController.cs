using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportsScheduler.API.Areas.Soccer.Models;
using SportsScheduler.API.Areas.Soccer.Scrapers;
using WebGrease.Css.Extensions;

namespace SportsScheduler.API.Areas.Soccer.Controllers
{
    public class SoccerEventsController : ApiController
    {
        [HttpGet]
        [Route("soccer/events")]
        public HttpResponseMessage Index()
        {
            var scrapers = SoccerConfig.Scrapers();

            var soccerEvents = new List<ISoccerEvent>();
            scrapers.ForEach(scraper => soccerEvents.AddRange(scraper.Scrape()));

            return Request.CreateResponse(HttpStatusCode.OK, soccerEvents);
        }
    }
}