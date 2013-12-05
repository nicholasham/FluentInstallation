using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FluentInstallation
{
    class AssemblyInstallerFactoryLoader : IInstallerFactoryLoader
    {
        private readonly IAssemblyContext _assemblyContext;

        public AssemblyInstallerFactoryLoader(IAssemblyContext assemblyContext)
        {
            _assemblyContext = assemblyContext;
        }

        public IInstallerFactory Load()
        {

            if (!File.Exists(_assemblyContext.AssemblyFile))
            {
                throw Exceptions.AssemblyNotFound(_assemblyContext.AssemblyFile);
            }

            var assembly = Assembly.LoadFile(_assemblyContext.AssemblyFile);

            var factory = assembly
                .GetTypes()
                .Where(type => typeof(IInstallerFactory).IsAssignableFrom(type))
                .Select(Activator.CreateInstance)
                .Cast<IInstallerFactory>()
                .FirstOrDefault();

            if (factory == null)
            {
                throw Exceptions.InstallerFactoryNotFoundInAssembly(assembly);
            }

            return factory;
        }
    }
}