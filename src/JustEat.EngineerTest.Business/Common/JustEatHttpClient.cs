using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JustEat.EngineerTest.Abstractions;

namespace JustEat.EngineerTest.Business.Common
{
    public class JustEatHttpClient : IHttpClient
    {
        public async Task<HttpResponseMessage> GetAsync(string baseUrl, params KeyValuePair<string, string>[] httpHeaders)
        {
            using (var client = new HttpClient())
            {
                if (httpHeaders.Any())
                {
                    foreach (var httpHeader in httpHeaders)
                    {
                        client.DefaultRequestHeaders.Add(httpHeader.Key, httpHeader.Value);
                    }
                }

                return await client.GetAsync(baseUrl);
            }
        }
    }
}
