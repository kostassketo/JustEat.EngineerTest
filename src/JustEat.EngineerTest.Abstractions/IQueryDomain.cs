using System.Threading.Tasks;

namespace JustEat.EngineerTest.Abstractions
{
    public interface IQueryDomain<T>
    {
        Task<T> GetAsync(string outcode);
    }
}
