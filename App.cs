using System;
using Xamarin.Forms;

namespace SportsScheduler
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			return new NavigationPage(new GenreOverview());
		}
	}
}

