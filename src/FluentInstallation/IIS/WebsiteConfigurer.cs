using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public class WebsiteConfigurer : IWebsiteConfigurer
    {
        private readonly Site _website;

        public WebsiteConfigurer(Site website)
        {
            
            if (website == null)
            {
                throw new ArgumentNullException("website");
            }

            _website = website;

        }

        public IWebsiteConfigurer Named(string name)
        {
            return Configure(site => site.Name = name);
        }

        public IWebsiteConfigurer UsingApplicationPool(string applicationPoolName)
        {
            return Configure(site => site.Applications.First().ApplicationPoolName = applicationPoolName);
        }

        public IWebsiteConfigurer OnPhysicalPath(string path)
        {
            return Configure(site => site.Applications["/"].VirtualDirectories["/"].PhysicalPath = path);
        }

        public IWebsiteConfigurer AddBinding(Action<IBindingConfigurer> binding)
        {
            return Configure(site =>
            {
                var configurer = new BindingConfigurer(site.Bindings.CreateElement());
                binding(configurer);
            });
        }

        public IWebsiteConfigurer AddApplication(Action<IApplicationConfigurer> application)
        {
            return Configure(site =>
            {
                var configurer = new ApplicationConfigurer(site.Applications.CreateElement());
                application(configurer);
            });
        }

        public IWebsiteConfigurer AddVirtualDirectory(Action<IVirtualDirectoryConfigurer> virtualDirectory)
        {
            return Configure(site =>
            {
                var configurer = new VirtualDirectoryConfigurer(site.Applications["/"].VirtualDirectories.CreateElement());
                virtualDirectory(configurer);
            });
        }

        public IWebsiteConfigurer Configure(Action<Site> action)
        {
            action(_website);
            return this;
        }
    }
}