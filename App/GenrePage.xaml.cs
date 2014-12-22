using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net.Http;

namespace SportsScheduler
{	
	public partial class GenrePage : ContentPage
	{	
		public GenrePage (Genre genre)
		{
			var soccerEvents = new SoccerEventsService ().Get ();

			var listView = new ListView
			{
				RowHeight = 40
			};

			listView.ItemsSource = soccerEvents;
			listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Title");

			Content = new StackLayout
			{
				Spacing = 10,
				Children = { listView }
			};
		}
	}
}

