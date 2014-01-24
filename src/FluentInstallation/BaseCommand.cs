using System.Collections;
using System.Management.Automation;

namespace FluentInstallation
{
    /// <summary>
    /// Base assemblyContext for install commands
    /// </summary>
    public abstract class BaseCommand :Cmdlet, ICommand
    {
        protected BaseCommand(IInstallerFactory installerFactory)
        {
            InstallerFactory = installerFactory;
            Parameters = new Hashtable();
        }

        protected BaseCommand() 
        {
            var assemblyLoader = new AssemblyLoader(() => this.AssemblyFile);
            InstallerFactory = new InstallerFactory(assemblyLoader.Load);
            Parameters = new Hashtable();
        }

        protected IInstallerFactory InstallerFactory { get; set; }

        [Parameter(Mandatory = true)]
        public string AssemblyFile { get; set; }

        [Parameter(Mandatory = false)]
        public Hashtable Parameters { get; set; }
    }
}