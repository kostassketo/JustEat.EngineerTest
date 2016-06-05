using System;
using System.Collections.Generic;
using Castle.Windsor.Installer;

namespace JustEat.EngineerTest.IoC
{
    public class JustEatInstallerFactory : InstallerFactory
    {
        public override IEnumerable<Type> Select(IEnumerable<Type> installerTypes)
        {
            return installerTypes;
        }
    }
}
