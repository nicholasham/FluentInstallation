using System.Collections.Generic;
using System.Management.Automation;
using Debugger = System.Diagnostics.Debugger;

namespace FluentInstallation
{
    /// <summary>
    ///     Installs
    /// </summary>
    [Cmdlet(VerbsLifecycle.Install, "Fluent")]
    public class InstallCommand : BaseCommand
    {
        public InstallCommand(IInstallerFactoryFinder finder) : base(finder)
        {
        }

        public InstallCommand() 
        {
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            
            IInstallerFactory factory = Finder.Find();
            IEnumerable<IInstaller> installers = factory.Create();

            var context = new InstallerContext(this);

            foreach (IInstaller installer in installers)
            {
                installer.Install(context);
            }
        }
    }
}