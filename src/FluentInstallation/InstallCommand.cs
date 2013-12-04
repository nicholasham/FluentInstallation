using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Reflection;

namespace FluentInstallation
{
    public interface IInstallerFinder
    {
        IEnumerable<IInstaller> FindInAssembly(Assembly assembly);
    }

    public class InstallerFinder : IInstallerFinder
    {
        public IEnumerable<IInstaller> FindInAssembly(Assembly assembly)
        {
            return new List<IInstaller>();
        }
    }

    public abstract class InstallationCommand :Cmdlet
    {
        protected InstallationCommand(IInstallerFinder installerFinder)
        {
            Finder = installerFinder;
        }

        protected InstallationCommand() : this(new InstallerFinder())
        {
            
        }

        public IInstallerFinder Finder { get; set; }


        public string AssemblyFile { get; set; }
    }

    /// <summary>
    /// Installs
    /// </summary>
    [Cmdlet(VerbsLifecycle.Install, "Fluent")]
    public class InstallCommand : InstallationCommand
    {
        public InstallCommand(IInstallerFinder installerFinder) : base(installerFinder)
        {
        }

        public InstallCommand()
        {
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (!File.Exists(AssemblyFile))
            {
                throw Errors.AssemblyNotFound(AssemblyFile);
            }

            var assembly = Assembly.LoadFile(AssemblyFile);
            var installers = Finder.FindInAssembly(assembly);

            foreach (var installer in installers)
            {
            }

        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            
        }
    }
}