using SportsScheduler.ViewModels;
using Xamarin.Forms;

namespace SportsScheduler.Views
{
    public class BaseView : ContentPage
    {
        public BaseView()
        {
            SetBinding(TitleProperty, new Binding(BaseViewModel.TitlePropertyName));
            SetBinding(IconProperty, new Binding(BaseViewModel.IconPropertyName));

            BackgroundColor = Color.White;
        }
    }
}
