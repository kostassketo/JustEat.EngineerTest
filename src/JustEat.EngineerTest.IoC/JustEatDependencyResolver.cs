using System.Reflection;
using System.Web.Http.Dispatcher;
using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace JustEat.EngineerTest.IoC
{
    public class JustEatDependencyResolver
    {
        internal static IWindsorContainer Container { get; private set; }

        internal static Assembly CallingAssembly { get; private set; }

        public static void Register(Assembly callingAssembly)
        {
            Register(callingAssembly, null);
        }

        public static IHttpControllerActivator CreateApiControllerActivator()
        {
            return new JustEatApiControllerActivator(Container);
        }

        internal static void Register(Assembly callingAssembly, ComponentModelDelegate componentModelDelegate)
        {
            CallingAssembly = callingAssembly;

            var container = new WindsorContainer();
            container.Install(FromAssembly.This(new JustEatInstallerFactory()));
            Container = container;
        }
    }
}
