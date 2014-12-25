using System.Collections.ObjectModel;
using SportsScheduler.Models;

namespace SportsScheduler.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }
        public MenuViewModel()
        {
            Title = "Sports Scheduler";
            MenuItems = new ObservableCollection<MenuItem>
                        {
                            new MenuItem {Id = 0, Title = "Home", MenuType = MenuType.Home, Icon = "homemenu.png"},
                            new MenuItem {Id = 1, Title = "Soccer", MenuType = MenuType.Soccer, Icon = "soccermenu.png"},
                            new MenuItem {Id = 2, Title = "Tennis", MenuType = MenuType.Tennis, Icon = "tennismenu.png"}
                        };
        }

        public string Title { get; set; }
    }
}
