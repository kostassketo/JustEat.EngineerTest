using System.Collections.Generic;
using System.Threading.Tasks;
using JustEat.EngineerTest.Abstractions;
using static JustEat.EngineerTest.Business.Common.HttpHeader;
using JustEat.EngineerTest.Domain.Dto;

namespace JustEat.EngineerTest.Business.Managers
{
    public class RestaurantsManager : IQueryDomain<RestaurantResults>
    {
        private readonly IHttpClient _httpClient;
        private readonly IWebConfig _webConfig;
        private readonly IJsonDeserializer _jsonDeserializer;

        public RestaurantsManager(IHttpClient httpClient, IWebConfig webConfig, IJsonDeserializer jsonDeserializer)
        {
            _httpClient = httpClient;
            _webConfig = webConfig;
            _jsonDeserializer = jsonDeserializer;
        }

        public async Task<RestaurantResults> GetAsync(string outcode)
        {
            if (string.IsNullOrEmpty(outcode))
            {
                return new RestaurantResults();
            }

            var headers = new[]
            {
                new KeyValuePair<string, string>(AcceptLanguage, _webConfig.HeaderAcceptLanguageValue),
                new KeyValuePair<string, string>(AcceptTenant, _webConfig.HeaderAcceptTenantValue),
                new KeyValuePair<string, string>(Authorization, _webConfig.HeaderAuthorizationValue),
                new KeyValuePair<string, string>(Host, _webConfig.HeaderHostValue)
            };

            var restaurantsApiUrl = $"{_webConfig.RestaurantsApiUrl}{outcode}";
            var restaurants = await (await _httpClient.GetAsync(restaurantsApiUrl, headers)).Content.ReadAsStringAsync();

            return _jsonDeserializer.Deserialize<RestaurantResults>(restaurants);
        }
    }
}
