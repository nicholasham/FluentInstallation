using System.Collections.Generic;
using System.Management.Automation;

namespace FluentInstallation
{
    /// <summary>
    ///     Uninstalls
    /// </summary>
    [Cmdlet(VerbsLifecycle.Uninstall, "Fluent")]
    public class UninstallCommand : BaseCommand
    {
        public UninstallCommand(IInstallerFactoryFinder finder)
            : base(finder)
        {
        }

        public UninstallCommand()
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
                installer.Uninstall(context);
            }
        }
    }
}