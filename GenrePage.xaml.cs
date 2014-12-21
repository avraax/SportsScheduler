using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SportsScheduler
{	
	public partial class GenrePage : ContentPage
	{	
		public GenrePage (Genre genre)
		{
			Content = new StackLayout
			{
				Spacing = 10,
				Children = { 
					new Label
					{
						Text = genre.Title,
						BackgroundColor = Color.Red,
						Font = Font.SystemFontOfSize (20)
					}
				}
			};
		}
	}
}

