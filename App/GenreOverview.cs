using System;
using Xamarin.Forms;

namespace SportsScheduler
{
	public class GenreOverview : ContentPage
	{
		public GenreOverview()
		{
			var listView = new ListView
			{
				RowHeight = 40
			};

			listView.ItemsSource = new Genre [] {
				new Genre {Title = "Soccer", Logo = "", GenreType = GenreType.Soccer},
				new Genre {Title = "Tennis", Logo = "", GenreType = GenreType.Tennis}
			};

			listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Title");

			listView.ItemSelected += async (sender, e) => {
				var genre = (Genre)e.SelectedItem;
				var todoPage = new GenrePage(genre);
				await Navigation.PushAsync(todoPage);
			};

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { listView }
			};
		}
	}

 	public class Genre {
		public string Title { get; set; }
		public string Logo { get; set; }
		public GenreType GenreType { get; set; }
	}

	public enum GenreType {
		Soccer = 0,
		Tennis = 1,
	}
}

