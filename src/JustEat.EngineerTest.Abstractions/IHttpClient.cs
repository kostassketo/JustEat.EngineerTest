using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JustEat.EngineerTest.Abstractions
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string baseUrl, params KeyValuePair<string, string>[] httpHeaders);
    }
}
