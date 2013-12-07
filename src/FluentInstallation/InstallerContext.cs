using System.Linq.Expressions;
using System.Management.Automation;

namespace FluentInstallation
{
    public class InstallerContext : IInstallerContext
    {
        internal ICommand Command { get; set; }

        public InstallerContext()
        {
            
        }
        
        public void WriteDebug(string message)
        {
            Command.WriteDebug(message);
        }

        public void WriteVerbose(string message)
        {
            Command.WriteVerbose(message);
        }

        public void WriteWarning(string message)
        {
            Command.WriteWarning(message);
        }

        public void WriteCommandDetail(string message)
        {
            Command.WriteCommandDetail(message);
        }
    }


}