using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace JustEat.EngineerTest.IoC.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (container == null)
            {
                return;
            }

            var assembly = JustEatDependencyResolver.CallingAssembly;
            container.Register(Classes.FromAssembly(assembly).InNamespace("JustEat.EngineerTest.Website.Controllers").LifestyleTransient());
        }
    }
}
