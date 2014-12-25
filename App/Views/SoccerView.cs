using System.Threading.Tasks;
using SportsScheduler.Services;
using Xamarin.Forms;

namespace SportsScheduler.Views
{
    public class SoccerView : BaseView
    {
        public SoccerView()
        {
            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10
            };

            var image = new Image{Source = ImageSource.FromFile("soccer.jpg")};
            stack.Children.Add(image);
            
            var heading = new Label
            {
                Font = Font.BoldSystemFontOfSize(NamedSize.Large),
                TextColor = Color.Black,
                Text = "Soccer matches",
                LineBreakMode = LineBreakMode.WordWrap
            };

            var scrollable = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 10,
                Padding = 10
            };
            scrollable.Children.Add(heading);

            var matches = CreateMatchesListView();
            scrollable.Children.Add(matches);

            stack.Children.Add(new ScrollView { VerticalOptions = LayoutOptions.FillAndExpand, Content = scrollable });

            Content = stack;
        }

        private ListView CreateMatchesListView()
        {
            var listView = new ListView
            {
                RowHeight = 40,
                ItemTemplate = new DataTemplate(typeof(TextCell))
            };

            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Title");
            listView.ItemTapped += async (sender, e) =>
            {
                var soccerEvent = (SoccerEvent)e.Item;
                var detailsPage = new DetailsPage(soccerEvent.EventId);
                await Navigation.PushAsync(detailsPage);
            };

            var soccerEvents = new SoccerEventsService().Get();
            soccerEvents.ContinueWith((task) =>
                                      {
                                          listView.ItemsSource = task.Result;
                                      },
                TaskScheduler.FromCurrentSynchronizationContext());

            return listView;
        }
    }
}
