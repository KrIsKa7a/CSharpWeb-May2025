namespace CinemaApp.Services.Tests
{
    using MockQueryable;
    using Moq;

    using Core;
    using Core.Interfaces;
    using Data.Models;
    using Data.Repository.Interfaces;
    using Web.ViewModels.Cinema;

    [TestFixture]
    public class CinemaServiceTests
    {
        private Mock<ICinemaRepository> cinemaRepositoryMock;
        private ICinemaService cinemaService;

        [SetUp]
        public void Setup()
        {
            this.cinemaRepositoryMock = new Mock<ICinemaRepository>(MockBehavior.Strict);
            this.cinemaService = new CinemaService(this.cinemaRepositoryMock.Object);
        }

        [Test]
        public void PassAlways()
        {
            // Test that will always pass to show that the SetUp is working
            Assert.Pass();
        }

        [Test]
        public async Task GetAllCinemasUserViewShouldReturnEmptyCollection()
        {
            List<Cinema> emptyCinemaList = new List<Cinema>();
            IQueryable<Cinema> emptyCinemaQueryable = emptyCinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(emptyCinemaQueryable);

            IEnumerable<UsersCinemaIndexViewModel> emptyViewModelColl = 
                await this.cinemaService
                .GetAllCinemasUserViewAsync();

            Assert.IsNotNull(emptyViewModelColl);
            Assert.AreEqual(emptyCinemaList.Count(), emptyViewModelColl.Count());
        }

        [Test]
        public async Task GetAllCinemasUserViewShouldReturnSameCollectionSizeWhenNonEmpty()
        {
            List<Cinema> cinemaList = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    Name = "CineMax",
                    Location = "Sofia",
                    IsDeleted = false,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
                new Cinema()
                {
                    Id = Guid.Parse("83388c3e-dd01-4268-b46a-e3151e464969"),
                    Name = "Cinema City",
                    Location = "Sofia",
                    IsDeleted = true,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
            };
            IQueryable<Cinema> cinemaQueryable = cinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(cinemaQueryable);

            IEnumerable<UsersCinemaIndexViewModel> viewModelCollection =
                await this.cinemaService
                    .GetAllCinemasUserViewAsync();

            Assert.IsNotNull(viewModelCollection);
            Assert.AreEqual(cinemaList.Count(), viewModelCollection.Count());
        }

        [Test]
        public async Task GetAllCinemasUserViewShouldReturnSameDataInViewModels()
        {
            List<Cinema> cinemaList = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    Name = "CineMax",
                    Location = "Sofia",
                    IsDeleted = false,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
                new Cinema()
                {
                    Id = Guid.Parse("83388c3e-dd01-4268-b46a-e3151e464969"),
                    Name = "Cinema City",
                    Location = "Sofia",
                    IsDeleted = true,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
            };
            IQueryable<Cinema> cinemaQueryable = cinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(cinemaQueryable);

            IEnumerable<UsersCinemaIndexViewModel> viewModelCollection =
                await this.cinemaService
                    .GetAllCinemasUserViewAsync();

            Assert.IsNotNull(viewModelCollection);
            Assert.AreEqual(cinemaList.Count(), viewModelCollection.Count());
            foreach (Cinema cinema in cinemaList)
            {
                UsersCinemaIndexViewModel? cinemaVm = viewModelCollection
                    .FirstOrDefault(vm => vm.Id.ToLower() == cinema.Id.ToString().ToLower());

                Assert.IsNotNull(cinemaVm);
                Assert.AreEqual(cinema.Name, cinemaVm!.Name, "Cinema name does not match between repository and ViewModel!");
                Assert.AreEqual(cinema.Location, cinemaVm.Location);
            }
        }

        [Test]
        public async Task GetAllCinemasUserViewShouldNotAddMoreDataThanPresent()
        {
            List<Cinema> cinemaList = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    Name = "CineMax",
                    Location = "Sofia",
                    IsDeleted = false,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                }
            };
            Cinema deletedCinema = new Cinema()
            {
                Id = Guid.Parse("83388c3e-dd01-4268-b46a-e3151e464969"),
                Name = "Cinema City",
                Location = "Sofia",
                IsDeleted = true,
                ManagerId = null,
                CinemaMovies = new List<CinemaMovie>(),
            };

            IQueryable<Cinema> cinemaQueryable = cinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(cinemaQueryable);

            IEnumerable<UsersCinemaIndexViewModel> viewModelCollection =
                await this.cinemaService
                    .GetAllCinemasUserViewAsync();

            Assert.IsNotNull(viewModelCollection);
            Assert.AreEqual(cinemaList.Count(), viewModelCollection.Count());
            foreach (UsersCinemaIndexViewModel cinemaVm in viewModelCollection)
            {
                Assert.AreNotEqual(deletedCinema.Id.ToString().ToLower(), cinemaVm.Id.ToLower(), "The deleted Cinema should not be present in the collection!");
            }
        }

        [Test]
        public async Task GetCinemaProgramAsyncShouldReturnNullWithNullCinemaId()
        {
            CinemaProgramViewModel? cinemaVm = await this.cinemaService
                .GetCinemaProgramAsync(null);

            Assert.IsNull(cinemaVm, "GetCinemaProgramAsync should return null with null cinemaId!");
        }

        [Test]
        public async Task GetCinemaProgramAsyncShouldReturnNullWithNonExistingCinemaId()
        {
            string nonExistingCinemaId = "018e23fa-4511-4ced-9532-bd2c200e57cb";
            List<Cinema> cinemaList = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    Name = "CineMax",
                    Location = "Sofia",
                    IsDeleted = false,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
                new Cinema()
                {
                    Id = Guid.Parse("83388c3e-dd01-4268-b46a-e3151e464969"),
                    Name = "Cinema City",
                    Location = "Sofia",
                    IsDeleted = true,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
            };
            IQueryable<Cinema> cinemaQueryable = cinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(cinemaQueryable);

            CinemaProgramViewModel? cinemaVm = await this.cinemaService
                .GetCinemaProgramAsync(nonExistingCinemaId);

            Assert.IsNull(cinemaVm, "GetCinemaProgramAsync should return null for non-existing Ids!");
        }

        [Test]
        public async Task GetCinemaProgramAsyncShouldReturnViewModelHavingCorrespondingDataWithValidId()
        {
            List<Cinema> cinemaList = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    Name = "CineMax",
                    Location = "Sofia",
                    IsDeleted = false,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
                new Cinema()
                {
                    Id = Guid.Parse("83388c3e-dd01-4268-b46a-e3151e464969"),
                    Name = "Cinema City",
                    Location = "Sofia",
                    IsDeleted = true,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
            };
            Cinema searchedCinema = cinemaList.First();
            string expectedCinemaData = searchedCinema.Name + " - " + searchedCinema.Location;
            int expectedMoviesCount = 0;
            IQueryable<Cinema> cinemaQueryable = cinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(cinemaQueryable);

            CinemaProgramViewModel? cinemaVm = await this.cinemaService
                .GetCinemaProgramAsync(searchedCinema.Id.ToString());

            Assert.IsNotNull(cinemaVm);
            Assert.AreEqual(searchedCinema.Name, cinemaVm!.CinemaName, "Cinema name should be copied to the ViewModel!");
            Assert.AreEqual(expectedCinemaData, cinemaVm.CinemaData, "Cinema data should be obtained for the ViewModel!");
            
            Assert.IsNotNull(cinemaVm.Movies);
            Assert.AreEqual(expectedMoviesCount, cinemaVm.Movies.Count(), "Cinema Movies should have count 0 when no Movies for the Cinema are available!");
        }

        [Test]
        public async Task GetCinemaProgramAsyncShouldReturnViewModelHavingCorrespondingDataAndMoviesWithValidId()
        {
            Movie movieInCinema = new Movie()
            {
                Id = Guid.Parse("7915b191-3fa3-4419-bcf5-5e555fba4b52"),
                Title = "Random Movie",
                ImageUrl = "https://myimageurl.com/",
                Director = "Mov Director",
            };
            List<CinemaMovie> cinemaMovies = new List<CinemaMovie>()
            {
                new CinemaMovie()
                {
                    Id = Guid.Parse("a2276de4-d7ef-48e8-9102-e14d3807c153"),
                    AvailableTickets = 50,
                    Showtime = "19:45",
                    IsDeleted = false,
                    CinemaId = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    MovieId = Guid.Parse("7915b191-3fa3-4419-bcf5-5e555fba4b52"),
                    Movie = movieInCinema,
                },
                new CinemaMovie()
                {
                    Id = Guid.Parse("7c2864a7-8e8c-4384-9bc6-2b8beb47a1ec"),
                    AvailableTickets = 55,
                    Showtime = "21:45",
                    IsDeleted = false,
                    CinemaId = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    MovieId = Guid.Parse("7915b191-3fa3-4419-bcf5-5e555fba4b52"),
                    Movie = movieInCinema,
                },
            };
            List<Cinema> cinemaList = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    Name = "CineMax",
                    Location = "Sofia",
                    IsDeleted = false,
                    ManagerId = null,
                    CinemaMovies = cinemaMovies
                },
                new Cinema()
                {
                    Id = Guid.Parse("83388c3e-dd01-4268-b46a-e3151e464969"),
                    Name = "Cinema City",
                    Location = "Sofia",
                    IsDeleted = true,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
            };
            Cinema searchedCinema = cinemaList.First();
            string expectedCinemaData = searchedCinema.Name + " - " + searchedCinema.Location;
            int expectedMoviesCount = 1;
            IQueryable<Cinema> cinemaQueryable = cinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(cinemaQueryable);

            CinemaProgramViewModel? cinemaVm = await this.cinemaService
                .GetCinemaProgramAsync(searchedCinema.Id.ToString());

            Assert.IsNotNull(cinemaVm);
            Assert.AreEqual(searchedCinema.Name, cinemaVm!.CinemaName, "Cinema name should be copied to the ViewModel!");
            Assert.AreEqual(expectedCinemaData, cinemaVm.CinemaData, "Cinema data should be obtained for the ViewModel!");
            
            Assert.IsNotNull(cinemaVm.Movies);
            Assert.AreEqual(expectedMoviesCount, cinemaVm.Movies.Count(), "Cinema Movies should take only the distinct entries!");

            CinemaProgramMovieViewModel cinemaFirstMovie = cinemaVm.Movies
                .First();
            Assert.AreEqual(movieInCinema.Id.ToString().ToLower(), cinemaFirstMovie.Id.ToLower(), "Id of the Movie in Cinema program should match!");
            Assert.AreEqual(movieInCinema.Title, cinemaFirstMovie.Title);
            Assert.AreEqual(movieInCinema.Director, cinemaFirstMovie.Director);
            Assert.AreEqual(movieInCinema.ImageUrl, cinemaFirstMovie.ImageUrl);
        }

        [Test]
        public async Task GetCinemaProgramAsyncShouldReturnViewModelHavingCorrespondingDataAndMoviesWithValidIdAndNullImageUrl()
        {
            Movie movieInCinema = new Movie()
            {
                Id = Guid.Parse("7915b191-3fa3-4419-bcf5-5e555fba4b52"),
                Title = "Random Movie",
                ImageUrl = null,
                Director = "Mov Director",
            };
            List<CinemaMovie> cinemaMovies = new List<CinemaMovie>()
            {
                new CinemaMovie()
                {
                    Id = Guid.Parse("a2276de4-d7ef-48e8-9102-e14d3807c153"),
                    AvailableTickets = 50,
                    Showtime = "19:45",
                    IsDeleted = false,
                    CinemaId = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    MovieId = Guid.Parse("7915b191-3fa3-4419-bcf5-5e555fba4b52"),
                    Movie = movieInCinema,
                },
                new CinemaMovie()
                {
                    Id = Guid.Parse("7c2864a7-8e8c-4384-9bc6-2b8beb47a1ec"),
                    AvailableTickets = 55,
                    Showtime = "21:45",
                    IsDeleted = false,
                    CinemaId = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    MovieId = Guid.Parse("7915b191-3fa3-4419-bcf5-5e555fba4b52"),
                    Movie = movieInCinema,
                },
            };
            List<Cinema> cinemaList = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    Name = "CineMax",
                    Location = "Sofia",
                    IsDeleted = false,
                    ManagerId = null,
                    CinemaMovies = cinemaMovies
                },
                new Cinema()
                {
                    Id = Guid.Parse("83388c3e-dd01-4268-b46a-e3151e464969"),
                    Name = "Cinema City",
                    Location = "Sofia",
                    IsDeleted = true,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
            };
            Cinema searchedCinema = cinemaList.First();
            string expectedCinemaData = searchedCinema.Name + " - " + searchedCinema.Location;
            int expectedMoviesCount = 1;
            string expectedImageUrl = "/images/no-image.jpg";
            IQueryable<Cinema> cinemaQueryable = cinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(cinemaQueryable);

            CinemaProgramViewModel? cinemaVm = await this.cinemaService
                .GetCinemaProgramAsync(searchedCinema.Id.ToString());

            Assert.IsNotNull(cinemaVm);
            Assert.AreEqual(searchedCinema.Name, cinemaVm!.CinemaName, "Cinema name should be copied to the ViewModel!");
            Assert.AreEqual(expectedCinemaData, cinemaVm.CinemaData, "Cinema data should be obtained for the ViewModel!");
            
            Assert.IsNotNull(cinemaVm.Movies);
            Assert.AreEqual(expectedMoviesCount, cinemaVm.Movies.Count(), "Cinema Movies should take only the distinct entries!");

            CinemaProgramMovieViewModel cinemaFirstMovie = cinemaVm.Movies
                .First();
            Assert.AreEqual(movieInCinema.Id.ToString().ToLower(), cinemaFirstMovie.Id.ToLower(), "Id of the Movie in Cinema program should match!");
            Assert.AreEqual(movieInCinema.Title, cinemaFirstMovie.Title);
            Assert.AreEqual(movieInCinema.Director, cinemaFirstMovie.Director);

            Assert.IsNotNull(cinemaFirstMovie.ImageUrl);
            Assert.AreEqual(expectedImageUrl, cinemaFirstMovie.ImageUrl);
        }

        [Test]
        public async Task GetCinemaDetailsAsyncShouldReturnNullWithNullCinemaId()
        {
            CinemaDetailsViewModel? cinemaVm = await this.cinemaService
                .GetCinemaDetailsAsync(null);

            Assert.IsNull(cinemaVm, "GetCinemaDetailsAsync should return null with null cinemaId!");
        }

        [Test]
        public async Task GetCinemaDetailsAsyncShouldReturnNullWithNonExistingCinemaId()
        {
            string nonExistingCinemaId = "018e23fa-4511-4ced-9532-bd2c200e57cb";
            List<Cinema> cinemaList = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    Name = "CineMax",
                    Location = "Sofia",
                    IsDeleted = false,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
                new Cinema()
                {
                    Id = Guid.Parse("83388c3e-dd01-4268-b46a-e3151e464969"),
                    Name = "Cinema City",
                    Location = "Sofia",
                    IsDeleted = true,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
            };
            IQueryable<Cinema> cinemaQueryable = cinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(cinemaQueryable);

            CinemaDetailsViewModel? cinemaVm = await this.cinemaService
                .GetCinemaDetailsAsync(nonExistingCinemaId);

            Assert.IsNull(cinemaVm, "GetCinemaProgramAsync should return null for non-existing Ids!");
        }

        [Test]
        public async Task GetCinemaDetailsAsyncShouldReturnViewModelHavingCorrespondingDataWithValidId()
        {
            List<Cinema> cinemaList = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    Name = "CineMax",
                    Location = "Sofia",
                    IsDeleted = false,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
                new Cinema()
                {
                    Id = Guid.Parse("83388c3e-dd01-4268-b46a-e3151e464969"),
                    Name = "Cinema City",
                    Location = "Sofia",
                    IsDeleted = true,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
            };
            Cinema searchedCinema = cinemaList.First();
            int expectedMoviesCount = 0;

            IQueryable<Cinema> cinemaQueryable = cinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(cinemaQueryable);

            CinemaDetailsViewModel? cinemaVm = await this.cinemaService
                .GetCinemaDetailsAsync(searchedCinema.Id.ToString());

            Assert.IsNotNull(cinemaVm);
            Assert.AreEqual(searchedCinema.Name, cinemaVm!.Name, "Cinema name should be copied to the ViewModel!");
            Assert.AreEqual(searchedCinema.Location, cinemaVm.Location, "Cinema location should be copied to the ViewModel!");
            
            Assert.IsNotNull(cinemaVm.Movies);
            Assert.AreEqual(expectedMoviesCount, cinemaVm.Movies.Count(), "Cinema Movies should have count 0 when no Movies for the Cinema are available!");
        }

        [Test]
        public async Task GetCinemaDetailsAsyncShouldReturnViewModelHavingCorrespondingDataAndMoviesWithValidId()
        {
            Movie movieInCinema = new Movie()
            {
                Id = Guid.Parse("7915b191-3fa3-4419-bcf5-5e555fba4b52"),
                Title = "Random Movie",
                ImageUrl = "https://myimageurl.com/",
                Director = "Mov Director",
                Duration = 150,
            };
            List<CinemaMovie> cinemaMovies = new List<CinemaMovie>()
            {
                new CinemaMovie()
                {
                    Id = Guid.Parse("a2276de4-d7ef-48e8-9102-e14d3807c153"),
                    AvailableTickets = 50,
                    Showtime = "19:45",
                    IsDeleted = false,
                    CinemaId = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    MovieId = Guid.Parse("7915b191-3fa3-4419-bcf5-5e555fba4b52"),
                    Movie = movieInCinema,
                },
                new CinemaMovie()
                {
                    Id = Guid.Parse("7c2864a7-8e8c-4384-9bc6-2b8beb47a1ec"),
                    AvailableTickets = 55,
                    Showtime = "21:45",
                    IsDeleted = false,
                    CinemaId = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    MovieId = Guid.Parse("7915b191-3fa3-4419-bcf5-5e555fba4b52"),
                    Movie = movieInCinema,
                },
            };
            List<Cinema> cinemaList = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = Guid.Parse("50fc7855-3fc5-4c4c-a494-29eaa51e1035"),
                    Name = "CineMax",
                    Location = "Sofia",
                    IsDeleted = false,
                    ManagerId = null,
                    CinemaMovies = cinemaMovies
                },
                new Cinema()
                {
                    Id = Guid.Parse("83388c3e-dd01-4268-b46a-e3151e464969"),
                    Name = "Cinema City",
                    Location = "Sofia",
                    IsDeleted = true,
                    ManagerId = null,
                    CinemaMovies = new List<CinemaMovie>(),
                },
            };
            Cinema searchedCinema = cinemaList.First();
            int expectedMoviesCount = 1;

            IQueryable<Cinema> cinemaQueryable = cinemaList
                .BuildMock();

            this.cinemaRepositoryMock
                .Setup(cr => cr.GetAllAttached())
                .Returns(cinemaQueryable);

            CinemaDetailsViewModel? cinemaVm = await this.cinemaService
                .GetCinemaDetailsAsync(searchedCinema.Id.ToString());

            Assert.IsNotNull(cinemaVm);
            Assert.AreEqual(searchedCinema.Name, cinemaVm!.Name, "Cinema name should be copied to the ViewModel!");
            Assert.AreEqual(searchedCinema.Location, cinemaVm.Location, "Cinema data should be obtained for the ViewModel!");
            
            Assert.IsNotNull(cinemaVm.Movies);
            Assert.AreEqual(expectedMoviesCount, cinemaVm.Movies.Count(), "Cinema Movies should take only the distinct entries!");

            CinemaDetailsMovieViewModel cinemaFirstMovie = cinemaVm.Movies
                .First();
            
            Assert.AreEqual(movieInCinema.Title, cinemaFirstMovie.Title);
            Assert.AreEqual(movieInCinema.Duration.ToString(), cinemaFirstMovie.Duration);
        }
    }
}
