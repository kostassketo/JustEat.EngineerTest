using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JustEat.EngineerTest.Abstractions;
using JustEat.EngineerTest.Business.Common;

namespace JustEat.EngineerTest.IoC.Installers
{
    public class CommonInstallerpublic : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (container == null)
            {
                return;
            }

            container.Register(Component.For<IHttpClient>().ImplementedBy<JustEatHttpClient>());
            container.Register(Component.For<IJsonDeserializer>().ImplementedBy<JustEatJsonConverter>());
        }
    }
}
