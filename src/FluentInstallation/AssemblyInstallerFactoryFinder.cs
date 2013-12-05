using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FluentInstallation
{
    class AssemblyInstallerFactoryFinder : IInstallerFactoryFinder
    {
        private readonly ICommand _command;

        public AssemblyInstallerFactoryFinder(ICommand command)
        {
            _command = command;
        }

        public IInstallerFactory Find()
        {

            if (!File.Exists(_command.AssemblyFile))
            {
                throw Exceptions.AssemblyNotFound(_command.AssemblyFile);
            }

            var assembly = Assembly.LoadFile(_command.AssemblyFile);

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