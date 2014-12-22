using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net.Http;
using SportsScheduler.Services;

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

			listView.ItemSelected += async (sender, e) => {
				var soccerEvent = (SoccerEvent)e.SelectedItem;
				var detailsPage = new DetailsPage(soccerEvent.EventId);
				await Navigation.PushAsync(detailsPage);
			};

			Content = new StackLayout
			{
				Spacing = 10,
				Children = { listView }
			};
		}
	}
}

