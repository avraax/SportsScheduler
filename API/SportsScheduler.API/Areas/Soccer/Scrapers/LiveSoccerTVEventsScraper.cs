using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using SportsScheduler.API.Areas.Soccer.Models;
using SportsScheduler.API.Areas.Soccer.Scrapers.Interfaces;
using SportsScheduler.API.Helper;

namespace SportsScheduler.API.Areas.Soccer.Scrapers
{
    public class LiveSoccerTvEventsScraper : ISoccerEventsScraper
    {
        private const string Referrer = "www.livesoccertv.com";
        private const string BaseUrl = "http://www.livesoccertv.com";
        private const string EventsUrl = "http://www.livesoccertv.com/schedules/{0}-{1}-{2}/";
        private const string EventDetailsUrl = "http://www.livesoccertv.com/match/{0}/";

        private readonly LiveSoccerEventsConfig _config;

        public LiveSoccerTvEventsScraper(LiveSoccerEventsConfig config)
        {
            _config = config;
        }

        public List<SoccerEvent> Scrape()
        {
            var doc = LoadDocument();
            var rowMatches = GetMatchRows(doc);

            return rowMatches == null ? new List<SoccerEvent>() 
                                      : rowMatches.Select(ToSoccerEvent).ToList();
        }

        public bool Enabled
        {
            get
            {
                return _config.Enabled;
            }
        }

        private HtmlDocument LoadDocument()
        {
            var cookie = new Cookie("u_order", "time")
            {
                Domain = ".livesoccertv.com",
                Expires = DateTime.Now.AddYears(1),
                Path = "/"
            };

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(new Uri(EventsUrl), cookie);
            var webClient = new WebClientEx(cookieContainer);
            using (var client = webClient)
            {
                client.Encoding = Encoding.UTF8;
                var date = DateTime.Now;
                var html = client.DownloadString(string.Format(EventsUrl, date.Year, date.Month, 26));

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                return doc;
            }
        }

        private IEnumerable<HtmlNode> GetMatchRows(HtmlDocument doc)
        {
            var liveCells = doc.DocumentNode.SelectNodes("//*[@id='matches']/table/tr[not(contains(@class, 'repeatrow'))]/td[@class='livecell']");
            if (liveCells == null || !liveCells.Any())
                return null;

            return liveCells.Select(htmlNode => htmlNode.ParentNode);
        }

        private SoccerEvent ToSoccerEvent(HtmlNode row)
        {
            var id = row.Attributes["id"].Value;
            var link = row.Descendants("a").First(x => x.Attributes.Contains("id") && x.Attributes["id"].Value == "g" + id);
            var ticks = row.Descendants("span").First(x => x.Attributes.Contains("dv")).Attributes["dv"].Value;
            return new SoccerEvent
            {
                Source = Referrer,
                EventId = id,
                Title = link.Attributes["title"].Value,
                Url = new Uri(BaseUrl + link.Attributes["href"].Value, UriKind.Absolute),
                StartTimeUtc = DateTimeHelper.FromMillisecondsSinceUnixEpoch(long.Parse(ticks))
            };
        }
    }

    public class LiveSoccerEventsConfig
    {
        public bool Enabled { get; set; }

        public LiveSoccerEventsConfig(bool enabled)
        {
            Enabled = enabled;
        }

        public static LiveSoccerEventsConfig FromConfig()
        {
            return new LiveSoccerEventsConfig(true);
        }
    }
}