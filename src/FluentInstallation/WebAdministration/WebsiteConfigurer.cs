using System;
using System.Linq;
using System.Reflection;
using Microsoft.Web.Administration;

namespace FluentInstallation.WebAdministration
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
            
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            return Configure(site => site.Name = name);
        }

        public IWebsiteConfigurer UseApplicationPool(string applicationPoolName)
        {
            return Configure(site => site.Applications.First().ApplicationPoolName = applicationPoolName);
        }

        public IWebsiteConfigurer OnPhysicalPath(string path)
        {
            return Configure(site => site.Application().VirtualDirectory().PhysicalPath = path);
        }

        public IWebsiteConfigurer UseWebProjectDirectoryAsPhysicalPath()
        {
            return OnPhysicalPath(Assembly.GetCallingAssembly().ParentDirectoryPath());
        }

        public IWebsiteConfigurer AddBinding(Action<IBindingConfigurer> action)
        {
            return Configure(site =>
            {
                var configurer = new BindingConfigurer(site.Bindings.CreateDefaultBinding());
                action(configurer);
            });
        }

        public IWebsiteConfigurer RemoveApplication(string alias)
        {

            return Configure(site =>
            {
                var foundApplication = site.Applications.Find(alias);

                if (foundApplication != null)
                {
                    site.Applications.Remove(foundApplication);
                }
            });
        }

        public IWebsiteConfigurer RemoveVirtualDirectory(string alias)
        {
            return Configure(site =>
            {
                var foundVirtualDirectory = site.Application().VirtualDirectories.FirstOrDefault(x => x.Path.Equals(alias.ToPath()));

                if (foundVirtualDirectory == null)
                {
                    throw Exceptions.VirtualDirectoryNotFoundInSite(site, alias);
                }

                site.Application().VirtualDirectories.Remove(foundVirtualDirectory);

            });
        }

        public IWebsiteConfigurer AddApplication(Action<IApplicationConfigurer> application)
        {
            return Configure(site =>
            {
                var configurer = new ApplicationConfigurer(site.Applications.CreateDefaultApplication());
                application(configurer);
            });
        }

        public IWebsiteConfigurer AddVirtualDirectory(Action<IVirtualDirectoryConfigurer> virtualDirectory)
        {
            return Configure(site =>
            {
                var configurer = new VirtualDirectoryConfigurer(site.Application().VirtualDirectories.CreateDefaultVirtualDirectory());
                virtualDirectory(configurer);
            });
        }

        public IWebsiteConfigurer AssertApplicationExists(string alias)
        {
            if (!_website.Applications.Exists(alias))
            {
                throw Exceptions.ApplicationNotFoundInSite(_website, alias);
            }

            return this;
        }

        public IWebsiteConfigurer AssertVirtualDirectoryExists(string alias)
        {
            if (!_website.Application().VirtualDirectories.Exists(alias))
            {
                throw Exceptions.VirtualDirectoryNotFoundInSite(_website, alias);
            }

            return this;
        }

        public IWebsiteConfigurer Configure(Action<Site> action)
        {
            action(_website);
            return this;
        }
    }
}