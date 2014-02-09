using System;
using System.Linq;
using Microsoft.Web.Administration;

namespace FluentInstallation.WebAdministration
{
    internal sealed class WebServerConfigurer : IWebServerConfigurer
    {

        public static Func<ApplicationPool, IApplicationPoolConfigurer> CreateApplicationPoolConfigurer = (configurer) => new ApplicationPoolConfigurer(configurer);
        public static Func<Application, IApplicationConfigurer> CreateApplicationConfigurer = (configurer) => new ApplicationConfigurer(configurer);
        
        public WebServerConfigurer(ILogger logger) 
            : this(logger, new WrappedServerManager())
        {            
        }

        internal WebServerConfigurer(ILogger logger, IServerManager serverManager)
        {
            Logger = logger;
            ServerManager = serverManager;
        }

        public IServerManager ServerManager { get; private set; }

        public ILogger Logger { get; private set; }

        public void Commit()
        {            
            ServerManager.CommitChanges();
        }

        public IWebServerConfigurer AddApplicationPool(Action<IApplicationPoolConfigurer> configurer)
        {
            var applicationPool = ServerManager.ApplicationPools.AddNewWithDefaults();
            configurer(CreateApplicationPoolConfigurer(applicationPool));
            Logger.Info(applicationPool.ContructAddMessage);

            return this;
        }

        public IWebServerConfigurer AddWebsite(Action<IWebsiteConfigurer> configurer)
        {
            var site = ServerManager.Sites.AddNewWithDefaults();
            configurer(new WebsiteConfigurer(Logger, site));
            Logger.Info(site.ContructAddMessage);
            return this;
        }

        public IWebServerConfigurer RemoveApplicationPool(string name)
        {
            var applicationPool = ServerManager.ApplicationPools.FirstOrDefault(appPool => appPool.Name == name);

            if (applicationPool != null)
            {
                ServerManager.ApplicationPools.Remove(applicationPool);
            }

            return this;
        }

        public IWebServerConfigurer RemoveWebsite(string name)
        {
            var webSite = ServerManager.Sites.FirstOrDefault(site => site.Name == name);
            if (webSite != null)
            {
                ServerManager.Sites.Remove(webSite);
            }

            return this;
        }


        public IWebServerConfigurer AlterApplicationPool(string name, Action<IApplicationPoolConfigurer> configurer)
        {
            var foundApplicationPool = ServerManager.ApplicationPools.FirstOrDefault(site => site.Name == name);

            if (foundApplicationPool == null)
            {
                throw Exceptions.NoSiteFoundMatchingName(name);
            }

            configurer(CreateApplicationPoolConfigurer(foundApplicationPool));

            return this;
        }


        public IWebServerConfigurer AlterWebsite(string name, Action<IWebsiteConfigurer> configurer)
        {
            var foundSite = ServerManager.Sites.FirstOrDefault(site => site.Name == name);

            if (foundSite == null)
            {
                throw Exceptions.NoSiteFoundMatchingName(name);
            }

            configurer( new WebsiteConfigurer(Logger, foundSite));

            return this;
        }

        public IWebServerConfigurer AssertWebsiteExists(string name)
        {
            if (!ServerManager.SiteExists(name))
            {
                throw Exceptions.NoSiteFoundMatchingName(name);
            }

            return this;
        }

        public IWebServerConfigurer AssertApplicationPoolExists(string name)
        {
            if (!ServerManager.ApplicationPoolExists(name))
            {
                throw Exceptions.NoApplicationPoolFoundMatchingName(name);
            }

            return this;
        }
    }
}