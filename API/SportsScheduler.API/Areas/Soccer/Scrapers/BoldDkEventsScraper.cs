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
    public class BoldDkEventsScraper : ISoccerEventsScraper
    {
        private const string Referrer = "www.bold.dk";
        private const string BaseUrl = "http://www.bold.dk";
        private const string Url = "http://www.bold.dk/tv/";

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
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                return client.DownloadString(string.Format(Url));
            }
        }

        private IEnumerable<HtmlNode> GetMatchesRows(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var rows = doc.DocumentNode.SelectNodes("//*[@class='tv']/tr");
            if (rows == null || !rows.Any())
                return null;

            return rows;
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