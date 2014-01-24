using System.Management.Automation;

namespace FluentInstallation
{
    /// <summary>
    ///     Uninstalls
    /// </summary>
    [Cmdlet(VerbsLifecycle.Uninstall, "Fluent")]
    public class UninstallCommand : BaseCommand
    {
        public UninstallCommand(IInstallerFactory installerFactory)
            : base(installerFactory)
        {
        }

        public UninstallCommand()
        {
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var context = new InstallerContext(Parameters, new CommandLogger(this));

            IInstaller installer = InstallerFactory.Create();
            installer.Uninstall(context);
        }
    }
}