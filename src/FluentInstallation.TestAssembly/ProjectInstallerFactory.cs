using System.Collections.Generic;
using System.Text;

namespace FluentInstallation.TestAssembly
{
    public class ProjectInstallerFactory : IInstallerFactory
    {
        public IEnumerable<IInstaller> Create()
        {
            return new[] {new ProjectInstaller()};
        }
    }
}