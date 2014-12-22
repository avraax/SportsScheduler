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

			var titleLabel = TitleLabel (eventDetails);
			var startTimeLabel = StartTime (eventDetails.StartTimeUtc);
			var channelsListView = ChannelsListView (eventDetails.Channels);

			Content = new StackLayout
			{
				Spacing = 10,
				Children = { titleLabel, startTimeLabel, channelsListView }
			};
		}

		private Label TitleLabel (SoccerEventDetails eventDetails)
		{
			return new Label {
				Text = eventDetails.HomeTeam + " - " + eventDetails.AwayTeam,
				Font = Font.SystemFontOfSize (20),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 30
			};
		}

		private Label StartTime (DateTime startTimeUtc)
		{
			return new Label {
				Text = "Start time (UTC): " + startTimeUtc.ToString("yyyy-MM-dd HH:mm:ss"),
				Font = Font.SystemFontOfSize (15),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 30
			};
		}

		private ListView ChannelsListView (IList<Channel> channels)
		{
			var channelsListView = new ListView {
				RowHeight = 40
			};
			channelsListView.ItemsSource = channels;
			channelsListView.ItemTemplate = new DataTemplate(typeof(TextCell));
			channelsListView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");

			return channelsListView;
		}
	}
}

