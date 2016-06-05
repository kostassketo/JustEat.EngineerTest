using System.Collections.Generic;

namespace JustEat.EngineerTest.Domain.Dto
{
    public class RestaurantResults
    {
        public IEnumerable<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}
