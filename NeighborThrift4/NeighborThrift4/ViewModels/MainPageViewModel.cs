using NeighborThrift4.Data;
using NeighborThrift4.Services;
using NeighborThrift4.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NeighborThrift4.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDataService dataService)
			: base(navigationService)
		{
			Title = "Main Page";
			Destinations = new List<string>(new[]
			{
				nameof(DetailPage),
				nameof(SecondPage),
				nameof(ThirdPage)
			});
			_pageDialogService = pageDialogService ?? throw new ArgumentNullException(nameof(pageDialogService));
			_dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));

			refresh();
		}

		public override async void OnNavigatedTo(INavigationParameters parameters)
		{
			if(parameters["action"] == "go back" && parameters["previousPage"] != null)
			{
				if (await _pageDialogService.DisplayAlertAsync("You're back?!", "I just barely sent you away and here you are back again.\nDo you want to stay or go back?", "Bounce me back!", "Stay here.  I'm tired.").ConfigureAwait(true))
				{
					await NavigationService.NavigateAsync((string)parameters["previousPage"], ("text", parameters["text"])).ConfigureAwait(true);
				}
			}
		}

		private string newDonorName;
		public string NewDonorName
		{
			get => newDonorName;
			set { SetProperty(ref newDonorName, value); }
		}

		private Command addDonorCommand;
		public Command AddDonorCommand => addDonorCommand ?? (addDonorCommand = new Command(() =>
		{
			var donor = new Donor()
			{
				Name = NewDonorName
			};

			_dataService.AddDonor(donor);
			refresh();
		}));

		private void refresh()
		{
			Donors = _dataService.GetDonors();
		}

		public IEnumerable<Donor> Donors { get; set; }

		public List<string> Destinations { get; private set; }
		public string SelectedDestination { get; set; }

		private string obj1;
		public string Obj1
		{
			get => obj1;
			set { SetProperty(ref obj1, value); }
		}

		private Command navigate;
		readonly IPageDialogService _pageDialogService;
		readonly IDataService _dataService;

		public Command Navigate => navigate ?? (navigate = new Command(async () =>
		{
			NavigationParameters parameters = new NavigationParameters();
			parameters.Add("text", Obj1);
			await TestableNavigation.TestableNavigateAsync(NavigationService, SelectedDestination, parameters, false, true).ConfigureAwait(false);
		}));
	}
}
