using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SportsScheduler.API.Areas.Soccer.Models;
using SportsScheduler.API.Helper;

namespace SportsScheduler.API.Areas.Soccer.Services
{
    public class SoccerEventDetailsScraper : ISoccerEventDetailsScraper
    {
        private const string Url = "http://www.livesoccertv.com/match/{0}/";

        public SoccerEventDetails EventDetails(string eventId)
        {
            HtmlDocument doc = LoadDocument(eventId);

            Teams teams = GetTeams(doc);
            IList<Channel> channels = GetChannels(doc);
            var ticks = GetTicks(doc);

            return new SoccerEventDetails
                   {
                       EventId = eventId,
                       StartTimeUtc = DateTimeHelper.FromMillisecondsSinceUnixEpoch(ticks),
                       HomeTeam = teams.HomeTeam,
                       AwayTeam = teams.AwayTeam,
                       Channels = channels
                   };
        }

        private long GetTicks(HtmlDocument doc)
        {
            var span = doc.DocumentNode.Descendants("span").First(x => x.Attributes.Contains("dv"));

            return long.Parse(span.Attributes["dv"].Value);
        }

        private IList<Channel> GetChannels(HtmlDocument doc)
        {
            var list = new List<Channel>();
            var rows = doc.DocumentNode.SelectNodes("//*[@id='wc_channels']/tr");
            if (rows == null)
                return list;

            foreach (var row in rows)
            {
                var td = row.Descendants("td").ToList();
                if (td.Count < 2)
                    continue;

                var country = td.First().Element("span").InnerText;
                var channelLinks = td.Last().Descendants("a");

                foreach (var channelLink in channelLinks)
                {
                    var channel = new Channel
                                  {
                                      Country = country,
                                      Name = channelLink.Attributes["title"].Value
                                  };
                    list.Add(channel);
                }
            }

            return list;
        }

        private Teams GetTeams(HtmlDocument doc)
        {
            var table = doc.DocumentNode.SelectNodes("//*[@id='team']");

            return new Teams
                   {
                       HomeTeam = table.First().Descendants("a").First().InnerText,
                       AwayTeam = table.Last().Descendants("a").First().InnerText
                   };
        }

        private HtmlDocument LoadDocument(string eventId)
        {
            var webClient = new CookieAwareWebClient();
            using (var client = webClient)
            {
                client.Encoding = Encoding.UTF8;
                var html = client.DownloadString(string.Format(Url, eventId));

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                return doc;
            }
        }

        private SoccerEventDetails ToSoccerEventDetails(HtmlNode row)
        {
            var id = row.Attributes["id"].Value;
            var link = row.Descendants("a").First(x => x.Attributes.Contains("id") && x.Attributes["id"].Value == "g" + id);
            var ticks = row.Descendants("span").First(x => x.Attributes.Contains("dv")).Attributes["dv"].Value;
            return new SoccerEventDetails();
        }

        class Teams
        {
            public string HomeTeam { get; set; }
            public string AwayTeam { get; set; }
        }
    }
}