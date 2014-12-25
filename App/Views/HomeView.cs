using Xamarin.Forms;

namespace SportsScheduler.Views
{
    public class HomeView : BaseView
    {
        public HomeView()
        {
            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10
            };

            var image = new Image
                        {
                            Source = ImageSource.FromFile("channels.png"), 
                            Aspect = Aspect.AspectFill
                        };

            stack.Children.Add(image);

            var stack2 = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 10,
                Padding = 10
            };

            var about = new Label
            {
                Font = Font.BoldSystemFontOfSize(NamedSize.Large),
                TextColor = Color.Black,
                Text = "Welcome to Sports Scheduler",
                LineBreakMode = LineBreakMode.WordWrap
            };


            stack2.Children.Add(about);

            stack.Children.Add(new ScrollView { VerticalOptions = LayoutOptions.FillAndExpand, Content = stack2 });

            Content = stack;
        }
    }
}
