namespace CinemaApp.Web.Tests
{
    using Controllers;
    using Services.Core.Interfaces;

    using Moq;
    using CinemaApp.Web.ViewModels.Cinema;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    [TestFixture]
    public class CinemaControllerTests
    {
        private Mock<ICinemaService> cinemaServiceMock;
        private CinemaController cinemaController;

        [SetUp]
        public void Setup()
        {
            this.cinemaServiceMock = new Mock<ICinemaService>(MockBehavior.Strict);
            this.cinemaController = new CinemaController(this.cinemaServiceMock.Object);
        }

        [Test]
        public void PassAlways()
        {
            Assert.Pass();
        }

        [Test]
        public async Task IndexShouldReturnViewWithDataFromCinemaService()
        {
            IEnumerable<UsersCinemaIndexViewModel> cinemaIndexViewModelCollection
                = new List<UsersCinemaIndexViewModel>()
                {
                    new UsersCinemaIndexViewModel()
                    {
                        Id = "c89b0728-9734-45a2-b1f9-a2947b193fbe",
                        Name = "CineMax",
                        Location = "Sofia"
                    },
                    new UsersCinemaIndexViewModel()
                    {
                        Id = "afcb27d2-142a-4763-ad56-cf54a1c42e68",
                        Name = "Cinema City",
                        Location = "Sofia"
                    },
                };

            this.cinemaServiceMock
                .Setup(cs => cs.GetAllCinemasUserViewAsync())
                .ReturnsAsync(cinemaIndexViewModelCollection);

            IActionResult result = await this.cinemaController.Index();
            
            Assert.IsInstanceOf<ViewResult>(result);

            ViewResult viewResult = (ViewResult)result;
            Assert
                .IsInstanceOf<IEnumerable<UsersCinemaIndexViewModel>>(viewResult.ViewData.Model);

            IEnumerable<UsersCinemaIndexViewModel> modelResult =
                (IEnumerable<UsersCinemaIndexViewModel>)viewResult.ViewData.Model;
            Assert.AreEqual(cinemaIndexViewModelCollection.Count(), modelResult.Count());

            foreach (UsersCinemaIndexViewModel viewModel in modelResult)
            {
                UsersCinemaIndexViewModel seedDataViewModel = cinemaIndexViewModelCollection
                    .First(vm => vm.Id.ToLower() == viewModel.Id.ToLower());

                Assert.IsNotNull(seedDataViewModel);
                Assert.AreEqual(seedDataViewModel.Name, viewModel.Name);
                Assert.AreEqual(seedDataViewModel.Location, viewModel.Location);
            }
        }

        [Test]
        public async Task IndexShouldRedirectToHomeWhenExceptionOccurs()
        {
            string expectedRedirectionController = "Home";
            string expectedRedirectionAction = "Index";
            string errorMessageTempDataKey = "error";

            this.cinemaServiceMock
                .Setup(cs => cs.GetAllCinemasUserViewAsync())
                .ThrowsAsync(new ArgumentException());
            
            DefaultHttpContext httpContext = new DefaultHttpContext();
            Mock<ITempDataProvider> tempDataProvider = new Mock<ITempDataProvider>();
            this.cinemaController.TempData 
                = new TempDataDictionary(httpContext, tempDataProvider.Object);

            IActionResult result = await this.cinemaController.Index();

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            RedirectToActionResult redirectionResult = (RedirectToActionResult)result;

            Assert.AreEqual(expectedRedirectionController, redirectionResult.ControllerName);
            Assert.AreEqual(expectedRedirectionAction, redirectionResult.ActionName);
            Assert.IsNotNull(this.cinemaController.TempData[errorMessageTempDataKey], "Action Index() should provide ErrorMessage when Redirecting due to internal Exception!");
        }
    }
}
