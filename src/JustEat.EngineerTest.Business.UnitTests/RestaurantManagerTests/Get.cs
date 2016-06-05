using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using JustEat.EngineerTest.Abstractions;
using static JustEat.EngineerTest.Business.Common.HttpHeader;
using JustEat.EngineerTest.Domain;
using JustEat.EngineerTest.Domain.Dto;
using Moq;
using static Newtonsoft.Json.JsonConvert;
using NUnit.Framework;

namespace JustEat.EngineerTest.Business.UnitTests.RestaurantManagerTests
{
    [TestFixture]
    public class Get
    {
        private IQueryDomain<RestaurantResults> _restaurantsManager;
        private Mock<IHttpClient> _mockHttpClient;
        private Mock<IJsonDeserializer> _mockJsonConverter; 
        private Mock<IWebConfig> _mockWebConfig;

        [SetUp]
        public void Setup()
        {
            _mockHttpClient = new Mock<IHttpClient>();
            _mockJsonConverter = new Mock<IJsonDeserializer>();
            _mockWebConfig = new Mock<IWebConfig>();
            _mockWebConfig.SetupGet(x => x.RestaurantsApiUrl).Returns("http://something.com/");
            _restaurantsManager = new Managers.RestaurantsManager(_mockHttpClient.Object, _mockWebConfig.Object, _mockJsonConverter.Object);
        }

        [Test]
        public async Task GetsRestaurants_ByOutcode()
        {
            const string outcode = "outcode";
            var apiUrl = $"{_mockWebConfig.Object.RestaurantsApiUrl}{outcode}";
            var restaurants = Builder<Restaurant>.CreateListOfSize(3).Build().ToList();
            _mockJsonConverter.Setup(x => x.Deserialize<IEnumerable<Restaurant>>(It.IsAny<string>()))
                .Returns(new List<Restaurant>());
            _mockHttpClient.Setup(x => x.GetAsync(apiUrl, It.IsAny<KeyValuePair<string, string>[]>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    Content = new StringContent(SerializeObject(restaurants))
                }).Verifiable();

            await _restaurantsManager.GetAsync(outcode);

            _mockHttpClient.Verify();
        }

        [Test]
        public async Task Deserializes_ReturnedRestaurants()
        {
            var restaurants = Builder<RestaurantResults>.CreateNew().Build();
            _mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>[]>()))
                           .ReturnsAsync(new HttpResponseMessage
                           {
                               Content = new StringContent(SerializeObject(restaurants))
                           });
            _mockJsonConverter.Setup(x => x.Deserialize<RestaurantResults>(It.IsAny<string>()))
                              .Returns(restaurants)
                              .Verifiable();

            await _restaurantsManager.GetAsync("some outcode");

            _mockJsonConverter.Verify();
        }

        [Test]
        public async Task Returns_TheCorrectDataResults()
        {
            var restaurants = Builder<RestaurantResults>.CreateNew().Build();
            _mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>[]>()))
                           .ReturnsAsync(new HttpResponseMessage
                           {
                               Content = new StringContent(SerializeObject(restaurants))
                           });
            string serializedRestaurants = null;
            _mockJsonConverter.Setup(x => x.Deserialize<RestaurantResults>(It.IsAny<string>()))
                              .Callback<string>(x => serializedRestaurants = x)
                              .Returns(restaurants);

            await _restaurantsManager.GetAsync("some outcode");

            serializedRestaurants.Should().Be(SerializeObject(restaurants));
        }

        [Test]
        public async Task CallsRestaurantsApi_WithTheCorrectHttpHeaders()
        {
            var restaurants = Builder<Restaurant>.CreateListOfSize(3).Build().ToList();
            var responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(SerializeObject(restaurants))
            };
            KeyValuePair<string, string>[] headers = null;
            _mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>[]>()))
                           .Callback<string, KeyValuePair<string, string>[]>((x, y) => headers = y)
                           .ReturnsAsync(responseMessage);

            await _restaurantsManager.GetAsync("outcode");

            var headerKeys = headers.Select(x => x.Key);
            headerKeys.Should().Contain(AcceptLanguage);
            headerKeys.Should().Contain(AcceptTenant);
            headerKeys.Should().Contain(Authorization);
            headerKeys.Should().Contain(Host);
        }

        [Test]
        public async Task AssignsTheCorrectValue_ToEveryHttpHeader()
        {
            var restaurants = Builder<Restaurant>.CreateListOfSize(3).Build().ToList();
            var responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(SerializeObject(restaurants))
            };
            KeyValuePair<string, string>[] headers = null;
            _mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>[]>()))
                           .Callback<string, KeyValuePair<string, string>[]>((outpost, httpHeaders) => headers = httpHeaders)
                           .ReturnsAsync(responseMessage);
            _mockWebConfig.SetupGet(x => x.HeaderAuthorizationValue).Returns("Authorization");
            _mockWebConfig.SetupGet(x => x.HeaderAcceptLanguageValue).Returns("AcceptLanguage");
            _mockWebConfig.SetupGet(x => x.HeaderAcceptTenantValue).Returns("AcceptTenant");
            _mockWebConfig.SetupGet(x => x.HeaderHostValue).Returns("Host");

            await _restaurantsManager.GetAsync("outcode");

            headers.Where(x => x.Key == AcceptLanguage).FirstOrDefault().Value.Should().Be("AcceptLanguage");
            headers.Where(x => x.Key == AcceptTenant).FirstOrDefault().Value.Should().Be("AcceptTenant");
            headers.Where(x => x.Key == Authorization).FirstOrDefault().Value.Should().Be("Authorization");
            headers.Where(x => x.Key == Host).FirstOrDefault().Value.Should().Be("Host");
        }

        [TestCase(null)]
        [TestCase("")]
        public async Task DoesNotCallRestaurantsApi_WhenOutpostParameter_IsNullOrEmpty(string outpost)
        {
            await _restaurantsManager.GetAsync(outpost);

            _mockHttpClient.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>[]>()), Times.Never);
        }
    }
}
