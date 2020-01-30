using NeighborThrift4.Services;
using NeighborThrift4.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NeighborThrift4.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        private string obj1;
        public string Obj1
        {
            get => obj1;
            set { SetProperty(ref obj1, value); }
        }

        private Command navigate;
        public Command Navigate => navigate ?? (navigate = new Command(async () =>
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("text", Obj1);
            await TestableNavigation.TestableNavigateAsync(NavigationService, nameof(DetailPage), parameters, false, true).ConfigureAwait(false);
        }));
    }
}
