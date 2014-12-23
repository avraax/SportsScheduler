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
        private const string Url = "http://www.livesoccertv.com/schedules/{0}-{1}-{2}/";

        public List<ISoccerEvent> Scrape()
        {
            var html = GetHtml();

            var rowMatches = GetMatchesRows(html);
            if (rowMatches == null)
                return new List<ISoccerEvent>();
            
            return rowMatches.Select(ToSoccerEvent).ToList();
        }

        private string GetHtml()
        {
            var cookie = new Cookie("u_order", "time")
                         {
                             Domain = ".livesoccertv.com", 
                             Expires = DateTime.Now.AddYears(1), 
                             Path = "/"
                         };

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(new Uri(Url), cookie);
            var webClient = new CookieAwareWebClient(cookieContainer);
            using (var client = webClient)
            {
                client.Encoding = Encoding.UTF8;
                var date = DateTime.Now;
                return client.DownloadString(string.Format(Url, date.Year, date.Month, 26));
            }
        }

        private IEnumerable<HtmlNode> GetMatchesRows(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var liveCells = doc.DocumentNode.SelectNodes("//*[@id='matches']/table/tr[not(contains(@class, 'repeatrow'))]/td[@class='livecell']");
            if (liveCells == null || !liveCells.Any())
                return null;

            return liveCells.Select(htmlNode => htmlNode.ParentNode);
        }

        private ISoccerEvent ToSoccerEvent(HtmlNode row)
        {
            var id = row.Attributes["id"].Value;
            var link = row.Descendants("a").First(x => x.Attributes.Contains("id") && x.Attributes["id"].Value == "g" + id);
            var ticks = row.Descendants("span").First(x => x.Attributes.Contains("dv")).Attributes["dv"].Value;
            return new SoccerEvent
            {
                Referrer = Referrer,
                EventId = id,
                Title = link.Attributes["title"].Value,
                Url = new Uri(BaseUrl + link.Attributes["href"].Value, UriKind.Absolute),
                StartTimeUtc = DateTimeHelper.FromMillisecondsSinceUnixEpoch(long.Parse(ticks))
            };
        }
    }
}