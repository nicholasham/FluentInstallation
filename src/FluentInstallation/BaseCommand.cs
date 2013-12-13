using System.Collections;
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
            Parameters = new Hashtable();
        }

        protected BaseCommand() 
        {
            Finder = new AssemblyInstallerFactoryFinder(this);
            Parameters = new Hashtable();
        }

        protected IInstallerFactoryFinder Finder { get; set; }

        [Parameter(Mandatory = true)]
        public string AssemblyFile { get; set; }

        [Parameter(Mandatory = false)]
        public Hashtable Parameters { get; set; }
    }
}