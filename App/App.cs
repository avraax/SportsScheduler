using Xamarin.Forms;

namespace SportsScheduler
{
	public class App
    {
        private static Page homeView;
		public static Page GetMainPage ()
		{
		    return homeView ?? (homeView = new MasterPage());
		    //return new NavigationPage(new GenreOverview());
		}
	}
}

