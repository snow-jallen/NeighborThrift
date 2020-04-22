using System;
using System.Collections.ObjectModel;
using NeighborThrift4.Models;
using NeighborThrift4.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace NeighborThrift4.ViewModels
{
	public class MenuPageViewModel : BindableBase
    {
        private INavigationService _navigationService;

        public ObservableCollection<MyMenuItem> MenuItems { get; }

        private MyMenuItem selectedMenuItem;
        public MyMenuItem SelectedMenuItem
        {
            get => selectedMenuItem;
            set => SetProperty(ref selectedMenuItem, value);
        }

        public DelegateCommand NavigateCommand { get; private set; }

        public MenuPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            MenuItems = new ObservableCollection<MyMenuItem>();

            MenuItems.Add(new MyMenuItem()
            {
                Icon = "Detail Page",
                PageName = nameof(DetailPage),
                Title = "Detail Page"
            });

            MenuItems.Add(new MyMenuItem()
            {
                Icon = "Second Page",
                PageName = nameof(SecondPage),
                Title = "Second Page"
            });

            MenuItems.Add(new MyMenuItem()
            {
                Icon = "Third Page",
                PageName = nameof(ThirdPage),
                Title = "Third"
            });

            NavigateCommand = new DelegateCommand(Navigate);
        }

        async void Navigate()
        {
            await _navigationService.NavigateAsync(nameof(NavigationPage) + "/" + SelectedMenuItem.PageName);
        }
    }
}
