using System;
using System.Linq;
using System.Reflection;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web.Administration
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

        public static Site AddNewWithDefaults(this SiteCollection sites)
        {
            string defaultSiteName = string.Format("Site{0}", sites.Count + 1);
            string uniquePath = Assembly.GetCallingAssembly().ParentDirectoryPath();
            var site = sites.Add(defaultSiteName, uniquePath, 80);
            site.Id = sites.Max(x => x.Id) + 1;
            site.Bindings.Clear();
            return site;
        }

        public static Binding AddNewWithDefaults(this BindingCollection bindings)
        {
            const string protocol = "http";

            string bindingInformation = BindingInformation.Default().AssignNextAvailablePort().ToString();

            return bindings.Add(bindingInformation, protocol);
        }

        public static Application AddNewWithDefaults(this ApplicationCollection applications)
        {
            Application defaultApplication = applications.DefaultApplication();

            string path = "/application" + applications.Count;

            return applications.Add(path, defaultApplication.VirtualDirectory().PhysicalPath);
        }


        public static VirtualDirectory AddNewWithDefaults(this VirtualDirectoryCollection virtualDirectories)
        {
            VirtualDirectory defaultVirtualDirectory = virtualDirectories.DefaultVirtualDirectory();
            string path = "/virtualdirectories" + virtualDirectories.Count;
            return virtualDirectories.Add(path, defaultVirtualDirectory.PhysicalPath);
        }

        internal static BindingInformation ToBindingInformation(this Binding binding)
        {
            return BindingInformation.Parse(binding.BindingInformation);
        }

        internal static int GetHighestPort(this ServerManager serverManager)
        {
            var supportedProtocols = new[] {"http", "https"};

            var bindings = (from site in serverManager.Sites
                from binding in site.Bindings
                where supportedProtocols.Contains(binding.Protocol)
                select binding).ToList();

            return bindings.Max(x => x.ToBindingInformation().Port);
        }

        internal static int Increment(this int value)
        {
            return value + 1;
        }

        public static ApplicationPool AddNewWithDefaults(this ApplicationPoolCollection applicationPools)
        {
            string defaultName = string.Format("ApplicationPool{0}", applicationPools.Count + 1);
            ApplicationPool applicationPool = applicationPools.Add(defaultName);
            applicationPool.ManagedRuntimeVersion = "v4.0";

            return applicationPool;
        }

        public static bool Exists(this ApplicationCollection applications, string alias)
        {
            return applications.Find(alias) != null;
        }

        public static Application Find(this ApplicationCollection applications, string alias)
        {
            Application foundApplication = applications.FirstOrDefault(x => x.Path.Equals(alias.ToPath()));
            return foundApplication;
        }

        public static VirtualDirectory Find(this VirtualDirectoryCollection virtualDirectories, string alias)
        {
            VirtualDirectory foundVirtualDirectory =
                virtualDirectories.FirstOrDefault(x => x.Path.Equals(alias.ToPath()));
            return foundVirtualDirectory;
        }

        public static bool Exists(this VirtualDirectoryCollection virtualDirectories, string alias)
        {
            return virtualDirectories.Find(alias) != null;
        }

        public static string GetCertificateThumbprint(this Binding binding)
        {
            var hash = binding.CertificateHash;

            if (hash.Length > 0)
            {
                return BitConverter.ToString(hash).Replace("-", string.Empty);    
            }
            {
                return "Not specified";
            }
            
        }
    }
}