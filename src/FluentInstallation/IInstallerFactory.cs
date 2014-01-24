using System.Collections.Generic;
using System.Reflection;

namespace FluentInstallation
{
    public interface IInstallerFactory
    {
        IInstaller Create();
      
    }
}