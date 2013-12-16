using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace FluentInstallation
{
    public interface IInstallerContext
    {
        T GetParameters<T>() where T : class, new();
    }
}