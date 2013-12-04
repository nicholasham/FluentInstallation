using System.Collections;

namespace FluentInstallation
{
    public interface IInstallerContext
    {
        ILogger Logger { get; }
        Hashtable Parameters { get; }
    }
}