using System.Collections.Generic;

namespace JustEat.EngineerTest.Domain
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal RatingAverage { get; set; }

        public IEnumerable<CuisineType> CuisineTypes { get; set; } = new List<CuisineType>();
    }
}
