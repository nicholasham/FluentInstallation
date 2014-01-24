using System.Management.Automation;

namespace FluentInstallation
{
    /// <summary>
    ///     Installs
    /// </summary>
    [Cmdlet(VerbsLifecycle.Install, "Fluent")]
    public class InstallCommand : BaseCommand
    {
        public InstallCommand(IInstallerFactory installerFactory) : base(installerFactory)
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

            var context = new InstallerContext(Parameters, new CommandLogger(this));

            IInstaller installer = InstallerFactory.Create();
            installer.Install(context);
        }
    }
}