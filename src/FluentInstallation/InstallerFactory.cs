using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FluentInstallation
{
    public class InstallerFactory : IInstallerFactory
    {
        private readonly IAssemblyContext _assemblyContext;

        public InstallerFactory(IAssemblyContext assemblyContext)
        {
            _assemblyContext = assemblyContext;
        }
        
        public IEnumerable<IInstaller> Create()
        {
            if (!File.Exists(_assemblyContext.AssemblyFile))
            {
                throw Exceptions.AssemblyNotFound(_assemblyContext.AssemblyFile);
            }

            var assembly = Assembly.LoadFile(_assemblyContext.AssemblyFile);

            return assembly
                        .GetTypes()
                        .Where(type => typeof (IInstaller).IsAssignableFrom(type))
                        .Select(Activator.CreateInstance)
                        .Cast<IInstaller>()
                        .ToArray();
            
        }
    }
}