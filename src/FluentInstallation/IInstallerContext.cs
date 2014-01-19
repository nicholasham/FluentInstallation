using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FluentInstallation
{
    public interface IInstallerContext
    {
        IDictionary Parameters { get; }
      
        ILogger Logger { get; }
    }
}