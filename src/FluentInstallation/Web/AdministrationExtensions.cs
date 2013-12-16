using System.Linq;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web
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
        
        public static Binding CreateDefaultBinding(this BindingCollection bindings)
        {
            var protocol = "http";
            var bindingInformation = BindingInformation.Default().AssignNextAvailablePort().ToString();

            return bindings.Add(bindingInformation, protocol);
            
        }

        public static Application CreateDefaultApplication(this ApplicationCollection applications)
        {
            var defaultApplication = applications.DefaultApplication();

            var path = "/application" + applications.Count;

            return applications.Add(path, defaultApplication.VirtualDirectory().PhysicalPath);
        }


        public static VirtualDirectory CreateDefaultVirtualDirectory(this VirtualDirectoryCollection virtualDirectories)
        {
            var defaultVirtualDirectory = virtualDirectories.DefaultVirtualDirectory();
            var path = "/virtualdirectories" + virtualDirectories.Count;
            return virtualDirectories.Add(path, defaultVirtualDirectory.PhysicalPath);
        }

        internal static BindingInformation ToBindingInformation(this Binding binding)
        {
            return BindingInformation.Parse(binding.BindingInformation);
        }

        internal static int GetHighestPort(this ServerManager serverManager)
        {
            return serverManager.Sites.Select(site => site.Bindings.Max(x => x.ToBindingInformation().Port)).Max();
        }

        internal static int Increment(this int value)
        {
            return value + 1;
        }


        

        


    }
}