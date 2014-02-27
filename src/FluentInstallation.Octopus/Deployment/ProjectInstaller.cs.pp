using FluentInstallation;

namespace $rootnamespace$.Deployment
{
    public class ProjectInstaller : IInstaller
    {
        public void Install(IInstallerContext context)
        {
            var parameters = context.Parameters.Cast<ProjectParameters>();

        }

        public void Uninstall(IInstallerContext context)
        {
            
            var parameters = context.Parameters.Cast<ProjectParameters>();

        }
    }
}