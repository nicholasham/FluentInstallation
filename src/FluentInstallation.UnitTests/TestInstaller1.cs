using System;

namespace FluentInstallation
{
    [DefaultInstaller]
    public class TestInstaller1 : IInstaller
    {
        public void Install(IInstallerContext context)
        {
            throw new NotImplementedException();
        }

        public void Uninstall(IInstallerContext context)
        {
            throw new NotImplementedException();
        }
    }
}