using System.Threading.Tasks;
using Xamarin.Forms;
using SportsScheduler.Services;

namespace SportsScheduler
{
    public partial class GenrePage : ContentPage
    {
        public GenrePage(Genre genre)
        {
            var listView = SetupListView();

            Content = new StackLayout
            {
                Spacing = 10,
                Children = { listView }
            };

            var soccerEvents = new SoccerEventsService().Get();
            soccerEvents.ContinueWith((task) =>
                                      {
                                          listView.ItemsSource = task.Result;
                                      },
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        private ListView SetupListView()
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

            return listView;
        }
    }
}

