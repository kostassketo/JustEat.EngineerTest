using System.Threading.Tasks;
using System.Web.Http;
using JustEat.EngineerTest.Abstractions;
using JustEat.EngineerTest.Domain.Dto;

namespace JustEat.EngineerTest.Website.Controllers
{
    public class RestaurantsController : ApiController
    {
        private readonly IQueryDomain<RestaurantResults> _queryRestaurants;

        public RestaurantsController(IQueryDomain<RestaurantResults> queryRestaurants)
        {
            _queryRestaurants = queryRestaurants;
        }

        public async Task<RestaurantResults> Get(string outcode)
        {
            if (string.IsNullOrEmpty(outcode))
            {
                return new RestaurantResults();
            }

            return await _queryRestaurants.GetAsync(outcode);
        } 
    }
}
