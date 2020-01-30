using Moq;
using NeighborThrift4.Services;
using NeighborThrift4.ViewModels;
using NeighborThrift4.Views;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
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
			Xamarin.Forms.Mocks.MockForms.Init();
			Application.Current = new App();
		}

		[Test]
		public void TestNavigation()
		{
			var navServiceMock = new Mock<INavigationService>();

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

			var mainPage = new MainPageViewModel(navServiceMock.Object, pageDialogServiceMock.Object);
			mainPage.Obj1 = "this is a test";
			mainPage.SelectedDestination = mainPage.Destinations[2];
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
	}
}