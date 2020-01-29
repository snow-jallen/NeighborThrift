using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeighborThrift4.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {

        public DetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Obj2 = (string)parameters["text"];
        }

        private string obj2;
        public string Obj2
        {
            get => obj2;
            set { SetProperty(ref obj2, value); }
        }

    }
}
