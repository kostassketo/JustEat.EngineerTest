namespace JustEat.EngineerTest.Abstractions
{
    public interface IJsonDeserializer
    {
        T Deserialize<T>(string input);
    }
}
