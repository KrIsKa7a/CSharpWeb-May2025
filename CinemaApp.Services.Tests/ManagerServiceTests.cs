namespace CinemaApp.Services.Tests
{
    using System.Linq.Expressions;

    using MockQueryable;
    using Moq;

    using Core;
    using Core.Interfaces;
    using Data.Models;
    using Data.Repository.Interfaces;

    [TestFixture]
    public class ManagerServiceTests
    {
        private Mock<IManagerRepository> managerRepositoryMock;
        private IManagerService managerService;

        [SetUp]
        public void Setup()
        {
            this.managerRepositoryMock = new Mock<IManagerRepository>(MockBehavior.Strict);
            this.managerService = new ManagerService(this.managerRepositoryMock.Object);
        }

        [Test]
        public void PassAlways()
        {
            Assert.Pass();
        }

        [Test]
        public async Task GetIdByUserIdAsyncShouldReturnNullWithNullUserId()
        {
            Guid? managerId = await this.managerService
                .GetIdByUserIdAsync(null);

            Assert.IsNull(managerId);
        }

        [Test]
        public async Task GetIdByUserIdAsyncShouldReturnNullWithNonExistingUserId()
        {
            this.managerRepositoryMock
                .Setup(mr => mr.FirstOrDefaultAsync(It.IsAny<Expression<Func<Manager, bool>>>()))
                .Returns<Manager?>(null);

            Guid? managerId = await this.managerService
                .GetIdByUserIdAsync(null);

            Assert.IsNull(managerId);
        }

        [Test]
        public async Task GetIdByUserIdAsyncShouldManagerIdWithValidUserId()
        {
            string expectedTestManagerId = "d15a6f0c-a16c-487d-b536-c4948ba405aa";
            string testManagerUserId = "085f30e8-dd52-41f2-bb40-7c28e5d73aa7";
            Manager testManager = new Manager()
            {
                Id = Guid.Parse(expectedTestManagerId),
                IsDeleted = false,
                UserId = testManagerUserId,
                ManagedCinemas = new List<Cinema>(),
            };

            this.managerRepositoryMock
                .Setup(mr => mr.FirstOrDefaultAsync(It.IsAny<Expression<Func<Manager, bool>>>()))
                .ReturnsAsync(testManager);

            Guid? managerId = await this.managerService
                .GetIdByUserIdAsync(testManagerUserId);

            Assert.IsNotNull(managerId);
            Assert.AreEqual(expectedTestManagerId.ToLower(), managerId.ToString()!.ToLower());
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnFalseWithNullId()
        {
            bool exists = await this.managerService
                .ExistsByIdAsync(null);

            Assert.IsFalse(exists);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnFalseWithNonExistingId()
        {
            string nonExistingManagerId = "218f0cfc-510b-4b2e-a5ec-2fbf1aad5827";
            List<Manager> managersList = new List<Manager>()
            {
                new Manager()
                {
                    Id = Guid.Parse("527e5e28-7c7f-45e4-ba74-b3768f2ba26a"),
                    IsDeleted = false,
                    UserId = "ded63c7e-a7b5-4abb-868c-3ad116ecb139",
                    ManagedCinemas = new List<Cinema>(),
                },
                new Manager()
                {
                    Id = Guid.Parse("9e64c286-8efe-4e36-bbbe-49a188f47558"),
                    IsDeleted = false,
                    UserId = "75db3396-e799-4542-9358-87a91ab0e810",
                    ManagedCinemas = new List<Cinema>(),
                }
            };

            IQueryable<Manager> managersQueryableMock = managersList
                .BuildMock();

            this.managerRepositoryMock
                .Setup(mr => mr.GetAllAttached())
                .Returns(managersQueryableMock);

            bool exists = await this.managerService
                .ExistsByIdAsync(nonExistingManagerId);

            Assert.IsFalse(exists);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnTrueWithValidId()
        {
            List<Manager> managersList = new List<Manager>()
            {
                new Manager()
                {
                    Id = Guid.Parse("527e5e28-7c7f-45e4-ba74-b3768f2ba26a"),
                    IsDeleted = false,
                    UserId = "ded63c7e-a7b5-4abb-868c-3ad116ecb139",
                    ManagedCinemas = new List<Cinema>(),
                },
                new Manager()
                {
                    Id = Guid.Parse("9e64c286-8efe-4e36-bbbe-49a188f47558"),
                    IsDeleted = false,
                    UserId = "75db3396-e799-4542-9358-87a91ab0e810",
                    ManagedCinemas = new List<Cinema>(),
                }
            };
            string existingManagerId = managersList.First().Id.ToString();

            IQueryable<Manager> managersQueryableMock = managersList
                .BuildMock();

            this.managerRepositoryMock
                .Setup(mr => mr.GetAllAttached())
                .Returns(managersQueryableMock);

            bool exists = await this.managerService
                .ExistsByIdAsync(existingManagerId);

            Assert.IsTrue(exists);
        }

        [Test]
        public async Task ExistsByUserIdAsyncShouldReturnFalseWithNullUserId()
        {
            bool exists = await this.managerService
                .ExistsByUserIdAsync(null);

            Assert.IsFalse(exists);
        }

        [Test]
        public async Task ExistsByUserIdAsyncShouldReturnFalseWithNonExistingUserId()
        {
            string nonExistingUserId = "218f0cfc-510b-4b2e-a5ec-2fbf1aad5827";
            List<Manager> managersList = new List<Manager>()
            {
                new Manager()
                {
                    Id = Guid.Parse("527e5e28-7c7f-45e4-ba74-b3768f2ba26a"),
                    IsDeleted = false,
                    UserId = "ded63c7e-a7b5-4abb-868c-3ad116ecb139",
                    ManagedCinemas = new List<Cinema>(),
                },
                new Manager()
                {
                    Id = Guid.Parse("9e64c286-8efe-4e36-bbbe-49a188f47558"),
                    IsDeleted = false,
                    UserId = "75db3396-e799-4542-9358-87a91ab0e810",
                    ManagedCinemas = new List<Cinema>(),
                }
            };

            IQueryable<Manager> managersQueryableMock = managersList
                .BuildMock();

            this.managerRepositoryMock
                .Setup(mr => mr.GetAllAttached())
                .Returns(managersQueryableMock);

            bool exists = await this.managerService
                .ExistsByUserIdAsync(nonExistingUserId);

            Assert.IsFalse(exists);
        }

        [Test]
        public async Task ExistsByUserIdAsyncShouldReturnTrueWithValidUserId()
        {
            List<Manager> managersList = new List<Manager>()
            {
                new Manager()
                {
                    Id = Guid.Parse("527e5e28-7c7f-45e4-ba74-b3768f2ba26a"),
                    IsDeleted = false,
                    UserId = "ded63c7e-a7b5-4abb-868c-3ad116ecb139",
                    ManagedCinemas = new List<Cinema>(),
                },
                new Manager()
                {
                    Id = Guid.Parse("9e64c286-8efe-4e36-bbbe-49a188f47558"),
                    IsDeleted = false,
                    UserId = "75db3396-e799-4542-9358-87a91ab0e810",
                    ManagedCinemas = new List<Cinema>(),
                }
            };
            string existingUserId = managersList.First().UserId;

            IQueryable<Manager> managersQueryableMock = managersList
                .BuildMock();

            this.managerRepositoryMock
                .Setup(mr => mr.GetAllAttached())
                .Returns(managersQueryableMock);

            bool exists = await this.managerService
                .ExistsByUserIdAsync(existingUserId);

            Assert.IsTrue(exists);
        }
    }
}
