using JustEat.EngineerTest.Abstractions;
using static Newtonsoft.Json.JsonConvert;

namespace JustEat.EngineerTest.Business.Common
{
    public class JustEatJsonConverter : IJsonDeserializer
    {
        public T Deserialize<T>(string input)
        {
            return DeserializeObject<T>(input);
        }
    }
}
