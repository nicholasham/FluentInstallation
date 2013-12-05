using System.Collections.Generic;

namespace FluentInstallation.TestAssembly
{
    public class ProjectInstallerFactory : IInstallerFactory
    {
        public IEnumerable<IInstaller> Create()
        {
            return new List<IInstaller>();
        }
    }
}