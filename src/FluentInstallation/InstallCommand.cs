using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;

namespace FluentInstallation
{
    /// <summary>
    /// Installs
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
            
            var installers = Factory.Create();
            
            foreach (var installer in installers)
            {
                installer.Install(null);
            }

        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            
        }
    }
}