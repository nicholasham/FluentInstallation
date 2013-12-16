using System;
using System.Linq;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    internal class WebServerConfigurer : IWebServerConfigurer
    {
        
        public static Func<Site, IWebsiteConfigurer> CreateWebsiteConfigurer = (configurer) =>  new WebsiteConfigurer(configurer);
        public static Func<ApplicationPool, IApplicationPoolConfigurer> CreateApplicationPoolConfigurer = (configurer) => new ApplicationPoolConfigurer(configurer);
        public static Func<Application, IApplicationConfigurer> CreateApplicationConfigurer = (configurer) => new ApplicationConfigurer(configurer);
        
        public WebServerConfigurer()
            : this(new WrappedServerManager())
        {            
        }

        internal WebServerConfigurer(IServerManager serverManager)
        {
            ServerManager = serverManager;
        }

        public IServerManager ServerManager { get; private set; }

        public void Commit()
        {            
            ServerManager.CommitChanges();
        }

        public IWebServerConfigurer CreateApplicationPool(Action<IApplicationPoolConfigurer> configurer)
        {
            var defaultName = string.Format("ApplicationPool{0}", ServerManager.ApplicationPools.Count + 1);
            var applicationPool = ServerManager.ApplicationPools.Add(defaultName);
            configurer(CreateApplicationPoolConfigurer(applicationPool));
            return this;
        }

        public IWebServerConfigurer CreateWebsite(Action<IWebsiteConfigurer> website)
        {
            var defaultSiteName = string.Format("Site{0}", ServerManager.Sites.Count + 1);
            var uniquePath = string.Format("/{0}", Guid.NewGuid().ToString("N"));
            var site = ServerManager.Sites.Add(defaultSiteName, uniquePath, 80);
            website(CreateWebsiteConfigurer(site));
            return this;
        }

        public IWebServerConfigurer DeleteApplicationPool(string name)
        {
            var applicationPool = ServerManager.ApplicationPools.FirstOrDefault(appPool => appPool.Name == name);

            if (applicationPool != null)
            {
                ServerManager.ApplicationPools.Remove(applicationPool);
            }

            return this;
        }

        public IWebServerConfigurer DeleteWebsite(string name)
        {
            var webSite = ServerManager.Sites.FirstOrDefault(site => site.Name == name);
            if (webSite != null)
            {
                ServerManager.Sites.Remove(webSite);
            }

            return this;
        }

        public IWebServerConfigurer AlterWebsite(string name, Action<IWebsiteConfigurer> configurer)
        {
            var foundSite = ServerManager.Sites.FirstOrDefault(site => site.Name == name);

            if (foundSite == null)
            {
                throw Exceptions.NoSiteFoundMatchingName(name);
            }

            configurer(CreateWebsiteConfigurer(foundSite));

            return this;
        }

    }
}