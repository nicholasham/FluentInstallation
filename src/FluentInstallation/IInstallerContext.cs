using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace FluentInstallation
{
    public interface IInstallerContext
    {
        void Progress(string message);
        void WriteDebug(string message);
        void WriteVerbose(string message);
        void WriteWarning(string message);
        
    }
}