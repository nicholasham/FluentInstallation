using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace FluentInstallation
{
  

    public class InstallerFactory : IInstallerFactory
    {
        private  Func<Assembly> GetAssembly { get; set; }

        public InstallerFactory(Func<Assembly> getAssembly)
        {
            if (getAssembly == null)
            {
                throw new ArgumentNullException("getAssembly");
            }

            GetAssembly = getAssembly;
        }

        public IInstaller Create()
        {
            return GetAssembly()
                        .FindInstallerTypesMarkedAsDefault()
                        .Select(Activator.CreateInstance)
                        .Cast<IInstaller>()
                        .FirstOrDefault();
        }
    }
}