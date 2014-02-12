using System;
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

            var logger = new CommandLogger(this);
            var context = new InstallerContext(Parameters, logger);

            IInstaller installer = InstallerFactory.Create();

            try
            {
                installer.Uninstall(context);
            }
            catch (Exception exception)
            {
                logger.Error(exception);
            }
        }
    }
}