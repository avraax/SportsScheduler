using System;
using System.Collections.Generic;
using SportsScheduler.Helpers;
using SportsScheduler.Models;
using SportsScheduler.ViewModels;
using SportsScheduler.Views;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

namespace SportsScheduler
{
    public class MasterPage : MasterDetailPage
    {
        private MenuViewModel ViewModel
        {
            get { return BindingContext as MenuViewModel; }
        }

        readonly HomeMasterView _master;
        private readonly Dictionary<MenuType, NavigationPage> _pages;
        public MasterPage()
        {
            _pages = new Dictionary<MenuType, NavigationPage>();

            BindingContext = new MenuViewModel();

            Master = _master = new HomeMasterView(ViewModel);

            var homeNav = new NavigationPage(_master.PageSelection)
            {
                BarBackgroundColor = Helpers.Color.DarkBlue.ToFormsColor(),
                BarTextColor = Color.White
            };
            Detail = homeNav;
            _pages.Add(MenuType.Home, homeNav);

            _master.PageSelectionChanged = async (menuType) =>
            {

                if (Detail != null && Device.OS == TargetPlatform.WinPhone)
                {
                    await Detail.Navigation.PopToRootAsync();
                }

                NavigationPage newPage;
                if (_pages.ContainsKey(menuType))
                {
                    newPage = _pages[menuType];
                }
                else
                {
                    newPage = new NavigationPage(_master.PageSelection)
                    {
                        BarBackgroundColor = Helpers.Color.DarkBlue.ToFormsColor(),
                        BarTextColor = Color.White
                    };
                    _pages.Add(menuType, newPage);
                }
                Detail = newPage;
                IsPresented = false;
            };

            this.Icon = "slideout.png";

        }
    }
    public class HomeMasterView : BaseView
    {
        public Action<MenuType> PageSelectionChanged;
        private Page _pageSelection;
        private MenuType _menuType = MenuType.Soccer;
        public Page PageSelection
        {
            get { return _pageSelection; }
            set
            {
                _pageSelection = value;
                if (PageSelectionChanged != null)
                    PageSelectionChanged(_menuType);
            }
        }

        private readonly HomeView _homeView;
        private SoccerView _soccerView;
        private TennisView _tennisView;
        public HomeMasterView(MenuViewModel viewModel)
        {
            if (_homeView == null)
                _homeView = new HomeView();

            Icon = "slideout.png";
            BindingContext = viewModel;


            var layout = new StackLayout { Spacing = 0 };

            var label = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                BackgroundColor = Color.Transparent,
                Content = new Label
                {
                    Text = "MENU",
                    Font = Font.SystemFontOfSize(NamedSize.Medium)
                }
            };

            layout.Children.Add(label);

            var cell = new DataTemplate(typeof(MenuItemImageCell));
            cell.SetBinding(TextCell.TextProperty, BaseViewModel.TitlePropertyName);
            cell.SetBinding(ImageCell.ImageSourceProperty, "Icon");

            var listView = new ListView { ItemTemplate = cell, ItemsSource = viewModel.MenuItems };

            PageSelection = _soccerView;
            listView.ItemSelected += (sender, args) =>
            {
                var menuItem = listView.SelectedItem as MenuItem;
                _menuType = menuItem.MenuType;
                switch (menuItem.MenuType)
                {
                    case MenuType.Home:
                        PageSelection = _homeView;
                        break;
                    case MenuType.Soccer:
                        if (_soccerView == null)
                            _soccerView = new SoccerView();

                        PageSelection = _soccerView;
                        break;
                    case MenuType.Tennis:
                        if (_tennisView == null)
                            _tennisView = new TennisView();

                        PageSelection = _tennisView;
                        break;
                }
            };

            listView.SelectedItem = viewModel.MenuItems[0];
            layout.Children.Add(listView);

            BackgroundColor = Color.White;

            Content = layout;
        }
    }

}
