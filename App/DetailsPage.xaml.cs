using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SportsScheduler.Services;

namespace SportsScheduler
{	
	public partial class DetailsPage : ContentPage
	{	
		public DetailsPage (string eventId)
		{
			var eventDetails = new SoccerEventDetailsService ().Get (eventId);

			var channels = new ListView
			{
				RowHeight = 40
			};

			channels.ItemsSource = eventDetails.Channels;
			channels.ItemTemplate = new DataTemplate(typeof(TextCell));
			channels.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");

			var title = new Label
			{
				Text = eventDetails.HomeTeam + " - " + eventDetails.AwayTeam,
				Font = Font.SystemFontOfSize (20),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 30
			};

			Content = new StackLayout
			{
				Spacing = 10,
				Children = { title, channels }
			};
		}
	}
}

