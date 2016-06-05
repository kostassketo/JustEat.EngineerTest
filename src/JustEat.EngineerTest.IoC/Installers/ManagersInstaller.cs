using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JustEat.EngineerTest.Abstractions;
using JustEat.EngineerTest.Business.Managers;
using JustEat.EngineerTest.Domain.Dto;

namespace JustEat.EngineerTest.IoC.Installers
{
    public class ManagersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (container == null)
            {
                return;
            }

            container.Register(Component.For<IQueryDomain<RestaurantResults>>().ImplementedBy<RestaurantsManager>());
        }
    }
}
