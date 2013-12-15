using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public static class AdministrationExtensions
    {
        public static VirtualDirectory VirtualDirectory(this Application application)
        {
            return application.VirtualDirectories.DefaultVirtualDirectory();
        }

        public static VirtualDirectory DefaultVirtualDirectory(this VirtualDirectoryCollection virtualDirectories)
        {
            return virtualDirectories["/"];
        }

        public static Application DefaultApplication(this ApplicationCollection applications)
        {
            return applications["/"];
        }

        public static Application Application(this Site site)
        {
            return site.Applications.DefaultApplication();
        }

        public static string ToPath(this string alias)
        {
            return alias.StartsWith("/") ? alias : "/" + alias;
        }
    }
}