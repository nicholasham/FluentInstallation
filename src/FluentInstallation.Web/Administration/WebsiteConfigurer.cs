using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web.Administration
{
    public class WebsiteConfigurer : IWebsiteConfigurer
    {
        private readonly Site _website;

        public WebsiteConfigurer(ILogger logger, Site website)
        {

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            if (website == null)
            {
                throw new ArgumentNullException("website");
            }

            Logger = logger;
            _website = website;

        }

        public ILogger Logger { get; private set; }

        public IWebsiteConfigurer WithId(int id)
        {
            return Configure(site =>
            {
                site.Id = id;
            });
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
            if (!Directory.Exists(path))
            {
                throw Exceptions.PhysicalPathDoesNotExist(path);
            }

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
                var binding = site.Bindings.AddNewWithDefaults();
                action(new BindingConfigurer(binding));
                Logger.Info(binding.ContructAddMessage);
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
                    Logger.Info(foundApplication.ContructRemoveMessage);
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
                Logger.Info(foundVirtualDirectory.ContructRemoveMessage);

            });
        }

        public IWebsiteConfigurer AddApplication(Action<IApplicationConfigurer> configurer)
        {
            return Configure(site =>
            {
                Application application = site.Applications.AddNewWithDefaults();
                configurer(new ApplicationConfigurer(application));
                Logger.Info(application.ContructAddMessage);
            });
        }

        public IWebsiteConfigurer AddVirtualDirectory(Action<IVirtualDirectoryConfigurer> configurer)
        {
            return Configure(site =>
            {
                VirtualDirectory virtualDirectory = site.Application().VirtualDirectories.AddNewWithDefaults();
                configurer(new VirtualDirectoryConfigurer(virtualDirectory));
                Logger.Info(virtualDirectory.ContructAddMessage);
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