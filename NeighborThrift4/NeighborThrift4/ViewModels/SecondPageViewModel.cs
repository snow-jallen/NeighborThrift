using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeighborThrift4.ViewModels
{
	public class SecondPageViewModel : ViewModelBase
	{
		public SecondPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			_navigationService = navigationService;
		}
		readonly INavigationService _navigationService;
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
