using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using JustEat.EngineerTest.IoC;

namespace JustEat.EngineerTest.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            JustEatDependencyResolver.Register(Assembly.GetExecutingAssembly());
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), JustEatDependencyResolver.CreateApiControllerActivator());
        }
    }
}
