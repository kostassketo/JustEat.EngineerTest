using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using JustEat.EngineerTest.Abstractions;
using JustEat.EngineerTest.Domain.Dto;
using JustEat.EngineerTest.Website.Controllers;
using Moq;
using NUnit.Framework;

namespace JustEat.EngineerTest.Website.UnitTests.RestaurantsControllerTests
{
    [TestFixture]
    public class Get
    {
        private RestaurantsController _controller;
        private Mock<IQueryDomain<RestaurantResults>> _mockQueryRestaurant;

        [SetUp]
        public void Setup()
        {
            _mockQueryRestaurant = new Mock<IQueryDomain<RestaurantResults>>();
            _controller = new RestaurantsController(_mockQueryRestaurant.Object);
        }

        [Test]
        public async Task RetrievesRestaurants_ByOutCode()
        {
            const string outcode = "some outcode";
            _mockQueryRestaurant.Setup(x => x.GetAsync(outcode)).ReturnsAsync(new RestaurantResults()).Verifiable();

            await _controller.Get(outcode);

            _mockQueryRestaurant.Verify();
        }

        [Test]
        public async Task ReturnsTheCorrectDataResults()
        {
            var restaurants = Builder<RestaurantResults>.CreateNew().Build();
            _mockQueryRestaurant.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(restaurants);

            var results = await _controller.Get("some outcode");

            results.Should().Be(restaurants);
        }

        [TestCase(null)]
        [TestCase("")]
        public async Task ReturnsResultsWithNoRestaurants_WhenOutpostParameter_IsNullOrEmpty(string outpost)
        {
            var result = await _controller.Get(outpost);

            result.Restaurants.Should().BeEmpty();
        }
    }
}
