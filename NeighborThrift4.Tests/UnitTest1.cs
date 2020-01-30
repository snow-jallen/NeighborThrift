using Moq;
using NeighborThrift4.Services;
using NeighborThrift4.ViewModels;
using NeighborThrift4.Views;
using NUnit.Framework;
using Prism.Navigation;
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
        public void Test1()
        {

            Xamarin.Forms.Mocks.MockForms.OpenUriAction = (uri) =>
            {

            };

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

            var mainPage = new MainPageViewModel(navServiceMock.Object);
            mainPage.Obj1 = "this is a test";
            mainPage.Navigate.Execute(this);

            Assert.AreEqual(1, numberOfCalls);
            Assert.AreEqual(nameof(DetailPage), actualPageName);
            Assert.AreEqual("this is a test", actualNavParams["text"]);
        }
    }
}