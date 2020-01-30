using NeighborThrift4.Services;
using NeighborThrift4.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeighborThrift4.ViewModels
{
	public class ThirdPageViewModel : ViewModelBase
	{
		public ThirdPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			:base(navigationService)
		{
			_navigationService = navigationService;
			_pageDialogService = pageDialogService;
		}
		readonly INavigationService _navigationService;
		readonly IPageDialogService _pageDialogService;

		public override async void OnNavigatedTo(INavigationParameters parameters)
		{
			Obj2 = (string)parameters["text"];

			if(await _pageDialogService.DisplayAlertAsync("This is my title", "You navigated here, do you want to go back?", "Yes, go back", "No, stay here").ConfigureAwait(true))
			{
				//await _navigationService.GoBackAsync(("action", "go back"), ("previousPage", nameof(ThirdPage)), ("text", parameters["text"])).ConfigureAwait(true);
				await TestableNavigation.TestableGoBackAsyncWithParams(_navigationService, new NavigationParameters($"?action=go back&previousPage={nameof(ThirdPage)}&text={parameters["text"]}"), false, false).ConfigureAwait(true);
			}

		}

		private string obj2;

		public string Obj2
		{
			get => obj2;
			set { SetProperty(ref obj2, value); }
		}
	}
}
