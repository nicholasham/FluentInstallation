using System.Management.Automation;

namespace FluentInstallation
{
    /// <summary>
    /// Base assemblyContext for install commands
    /// </summary>
    public abstract class BaseCommand :Cmdlet, ICommand
    {
        protected BaseCommand(IInstallerFactoryFinder finder)
        {
            Finder = finder;
        }

        protected BaseCommand() 
        {
            Finder = new AssemblyInstallerFactoryFinder(this);
        }

        protected IInstallerFactoryFinder Finder { get; set; }

        [Parameter(Mandatory = true)]
        public string AssemblyFile { get; set; }
    }
}