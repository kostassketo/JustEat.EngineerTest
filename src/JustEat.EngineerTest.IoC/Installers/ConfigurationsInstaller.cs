using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JustEat.EngineerTest.Abstractions;
using JustEat.EngineerTest.Business.Configurations;

namespace JustEat.EngineerTest.IoC.Installers
{
    public class ConfigurationsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (container == null)
            {
                return;
            }

            container.Register(Component.For<IWebConfig>().ImplementedBy<WebConfig>().LifestyleSingleton());
        }
    }
}
