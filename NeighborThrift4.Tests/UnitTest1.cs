using Moq;
using NeighborThrift4.Data;
using NeighborThrift4.Services;
using NeighborThrift4.ViewModels;
using NeighborThrift4.Views;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace NeighborThrift4.Tests
{
	public class Tests
	{

		[SetUp]
		public void Setup()
		{
			//Xamarin.Forms.Mocks.MockForms.Init();
			//Application.Current = new App();
		}

		[Test]
		public void PutSomethingInAndGetItBack()
		{
			var fakeDonorList = new List<Donor>();
			var navServiceMock = new Mock<INavigationService>();
			var pageDialogMock = new Mock<IPageDialogService>();
			var dataService = new Mock<IDataService>();
			var notificationMock = new Mock<INotificationService>();
			dataService.Setup(m => m.AddDonor(It.IsAny<Donor>())).Callback<Donor>((donor) =>
			{
				fakeDonorList.Add(donor);
			});
			dataService.Setup(m => m.GetDonors()).Returns(fakeDonorList);

			var mainPageViewModel = new MainPageViewModel(navServiceMock.Object, pageDialogMock.Object, dataService.Object, notificationMock.Object, null);
			mainPageViewModel.NewDonorName = "New Donor";
			mainPageViewModel.AddDonorCommand.Execute(this);

			Assert.IsTrue(mainPageViewModel.Donors.Any(d => d.Name == "New Donor"));
		}

		[Test]
		public void TestNavigation()
		{
			var navServiceMock = new Mock<INavigationService>();
			var dataService = new Mock<IDataService>();
			var notificationMock = new Mock<INotificationService>();

			int numberOfCalls = 0;
			string actualPageName = string.Empty;
			INavigationParameters actualNavParams = null;
			TestableNavigation.TestableNavigateAsync = (navSvc, pageName, navParams, isModal, isAnimated) =>
			{
				++numberOfCalls;
				actualPageName = pageName;
				actualNavParams = navParams;
				return Task.FromResult<INavigationResult>(null);
			};

			var pageDialogServiceMock = new Mock<IPageDialogService>();

			var mainPage = new MainPageViewModel(navServiceMock.Object, pageDialogServiceMock.Object, dataService.Object, notificationMock.Object, null);
			mainPage.Obj1 = "this is a test";
			mainPage.SelectedDestination = mainPage.Destinations[1];
			mainPage.Navigate.Execute(this);

			Assert.AreEqual(1, numberOfCalls);
			Assert.AreEqual(nameof(ThirdPage), actualPageName);
			Assert.AreEqual("this is a test", actualNavParams["text"]);
		}

		[Test]
		public void TestDialog_GoBack()
		{
			var pageDialogServiceMock = new Mock<IPageDialogService>();
			pageDialogServiceMock.Setup(m => m.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult(true));

			var navServiceMock = new Mock<INavigationService>();
			int numberOfCalls = 0;
			INavigationParameters actualNavParams = null;
			TestableNavigation.TestableGoBackAsyncWithParams = (navSvc, navParams, isModal, isAnimated) =>
			{
				++numberOfCalls;
				actualNavParams = navParams;
				return Task.FromResult<INavigationResult>(null);
			};

			var thirdPage = new ThirdPageViewModel(navServiceMock.Object, pageDialogServiceMock.Object);
			thirdPage.OnNavigatedTo(new NavigationParameters("?action=go back"));

			Assert.AreEqual(1, numberOfCalls);
			Assert.AreEqual("go back", actualNavParams["action"]);
		}

		[Test]
		public void TestDialog_StayThere()
		{
			var pageDialogServiceMock = new Mock<IPageDialogService>();
			pageDialogServiceMock.Setup(m => m.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult(false));

			var navServiceMock = new Mock<INavigationService>();
			int numberOfCalls = 0;
			INavigationParameters actualNavParams = null;
			TestableNavigation.TestableGoBackAsyncWithParams = (navSvc, navParams, isModal, isAnimated) =>
			{
				++numberOfCalls;
				actualNavParams = navParams;
				return Task.FromResult<INavigationResult>(null);
			};

			var thirdPage = new ThirdPageViewModel(navServiceMock.Object, pageDialogServiceMock.Object);
			thirdPage.OnNavigatedTo(new NavigationParameters("?action=go back"));

			Assert.AreEqual(0, numberOfCalls);
		}

		[Test]

		public void TestNotification()
		{
			var notificationMock = new Mock<INotificationService>();
			notificationMock.Setup(m => m.Notify(It.IsAny<long[]>(), It.IsAny<string>()))
							.Returns<long[], string>((numbers, msg) =>
							{
								return numbers.Length;
							});
			var navigationMock = new Mock<INavigationService>();
			var pageDialogServiceMock = new Mock<IPageDialogService>();
			var databaseServiceMock = new Mock<IDataService>();


			var mainPage = new MainPageViewModel(navigationMock.Object, pageDialogServiceMock.Object, databaseServiceMock.Object, notificationMock.Object, null);
			mainPage.Number = 1234;
			mainPage.AddNumber.Execute(this);
			mainPage.Number = 5678;
			mainPage.AddNumber.Execute(this);
			mainPage.Number = 9123;
			mainPage.AddNumber.Execute(this);

			mainPage.SendNotification.Execute(this);


			Assert.IsTrue(mainPage.NumbersNotified == 3);
		}
	}
}