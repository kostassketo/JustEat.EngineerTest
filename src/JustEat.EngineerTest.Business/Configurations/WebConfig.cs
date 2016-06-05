using System.Configuration;
using JustEat.EngineerTest.Abstractions;

namespace JustEat.EngineerTest.Business.Configurations
{
    public class WebConfig : IWebConfig
    {
        public string HeaderAcceptTenantValue => ConfigurationManager.AppSettings["HttpHeader.AcceptTenant"];

        public string HeaderAcceptLanguageValue => ConfigurationManager.AppSettings["HttpHeader.AcceptLanguage"];

        public string HeaderAuthorizationValue => ConfigurationManager.AppSettings["HttpHeader.Authorization"];

        public string HeaderHostValue => ConfigurationManager.AppSettings["HttpHeader.Host"];

        public string RestaurantsApiUrl => ConfigurationManager.AppSettings["Api.Url.Restaurants"];
    }
}
