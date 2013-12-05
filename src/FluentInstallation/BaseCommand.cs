using System.Management.Automation;

namespace FluentInstallation
{
    /// <summary>
    /// Base assemblyContext for install commands
    /// </summary>
    public abstract class BaseCommand :Cmdlet, IAssemblyContext
    {
        protected BaseCommand(IInstallerFactory installerFactory)
        {
            Factory = installerFactory;
        }

        protected BaseCommand() 
        {
            Factory = new InstallerFactory(this);
        }

        protected IInstallerFactory Factory { get; set; }
        
        public string AssemblyFile { get; set; }
    }
}