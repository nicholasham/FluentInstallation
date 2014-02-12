using System;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;

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

            var logger = new CommandLogger(this);
            var context = new InstallerContext(Parameters, logger);

            IInstaller installer = InstallerFactory.Create();
            try
            {
                installer.Install(context);
            }
            catch (Exception exception)
            {
                logger.Error(exception);
            }
        }
    }
}