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

            var path = _command.AssemblyFile;

            if (!Directory.Exists(path))
            {
                path = Path.Combine(this.Assembly().DirectoryPath(), _command.AssemblyFile);
            }

            if (!File.Exists(path))
            {
                throw Exceptions.AssemblyNotFound(_command.AssemblyFile);
            }

            var assembly = Assembly.LoadFile(path);

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