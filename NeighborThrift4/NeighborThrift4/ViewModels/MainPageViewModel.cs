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

            PublicProperty = new Command(navigate_execute);
        }

        private async void navigate_execute()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("text", Obj1);
            await NavigationService.NavigateAsync(nameof(NeighborThrift4.Views.DetailPage), parameters);
        }

        private string obj1;
        public string Obj1
        {
            get => obj1;
            set { SetProperty(ref obj1, value); }
        }

        private Command backingField;

        public Command PublicProperty
        {
            get { return backingField; }
            set { backingField = value; }
        }

        //Short version:
        //======================================================================
        private Command navigate;


        // => means an auto-generated getter.
        // e.g. public int MyNum => 5;
        
        // ?? is null coalescing operator, if left hand side is not null return that.  Otherwise return right hand side.
        public Command Navigate => navigate ?? (navigate = new Command(async () =>
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("text", Obj1);
            await NavigationService.NavigateAsync(nameof(NeighborThrift4.Views.DetailPage), parameters);
        }));
    }
}
