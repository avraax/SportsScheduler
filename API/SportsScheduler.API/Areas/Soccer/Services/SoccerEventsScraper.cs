using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using SportsScheduler.API.Areas.Soccer.Models;

namespace SportsScheduler.API.Areas.Soccer.Services
{
    public class SoccerEventsScraper : ISoccerEventsScraper
    {
        private const string Url = "http://www.livesoccertv.com/schedules/{0}-{1}-{2}/";

        public IList<SoccerEvent> GetSoccerEvents()
        {
            var html = GetHtml();

            var rowMatches = GetMatchesRows(html);
            
            return rowMatches.Select(ToSoccerEvent).ToList();
        }

        private string GetHtml()
        {
            var date = DateTime.Now;
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                return client.DownloadString(string.Format(Url, date.Year, date.Month, date.Day));
            }
        }

        private IEnumerable<HtmlNode> GetMatchesRows(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var liveCells = doc.DocumentNode.SelectNodes("//*[@id='matches']/table/tr[not(contains(@class, 'repeatrow'))]/td[@class='livecell']").ToList();

            return liveCells.Select(htmlNode => htmlNode.ParentNode).ToList();
        }

        private SoccerEvent ToSoccerEvent(HtmlNode node)
        {
            return new SoccerEvent
            {
                Title = node.Descendants("a").First().InnerText
            };
        }
    }
}