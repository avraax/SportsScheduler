using System;
using HtmlAgilityPack;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SportsScheduler.Scraper
{
	public class Service
	{
		private readonly string url = "http://www.livesoccertv.com/schedules/{0}-{1}-{2}/";

		public IList<SoccerEvent> GetSoccerEvents() {
			var list = new List<SoccerEvent>();
			var html = GetHtml ();

			var rowMatches = GetMatchesRows (html);
			foreach(var node in rowMatches)
			{
				list.Add(ToSoccerEvent(node));
			}

			return list;
		}

		private string GetHtml ()
		{
			var date = DateTime.Now;
			using (var client = new HttpClient ()) {
				return client.GetStringAsync (string.Format (url, date.Year, date.Month, date.Day)).ConfigureAwait (false)
							 .GetAwaiter ()
					         .GetResult ();
			}
		}

		private IEnumerable<HtmlNode> GetMatchesRows(string html) {
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(html);

			return doc.DocumentNode.Element("html")
				                   .Element("body")
				                   .Descendants("tr")
					               .Where(node => node.Attributes.Contains("class") 
					                      && node.Attributes["class"].Value.Contains("matchrow"));
		}

		private SoccerEvent ToSoccerEvent(HtmlNode node) {
			return new SoccerEvent { 
				Title = node.Descendants("a").First().InnerText 
			};
		}
	}
}