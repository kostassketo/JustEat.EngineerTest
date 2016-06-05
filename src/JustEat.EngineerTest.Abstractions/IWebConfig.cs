namespace JustEat.EngineerTest.Abstractions
{
    public interface IWebConfig
    {
        string HeaderAcceptTenantValue { get; }

        string HeaderAcceptLanguageValue { get; }

        string HeaderAuthorizationValue { get; }

        string HeaderHostValue { get; }

        string RestaurantsApiUrl { get; }
    }
}
