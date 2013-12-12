using System;
using Microsoft.Web.Administration;
using System.Linq;

namespace FluentInstallation.IIS
{
    internal class WebServerConfigurer : IWebServerConfigurer
    {
        public WebServerConfigurer()
        {
            ServerManager = new ServerManager();
        }

        public ServerManager ServerManager { get; private set; }

        public void Commit()
        {            
            throw new NotImplementedException();
        }

        public IWebServerConfigurer CreateApplicationPool(Action<IApplicationPoolConfigurer> options)
        {
            var defaultName = string.Format("ApplicationPool{0}", ServerManager.ApplicationPools.Count + 1);
            var applicationPool = ServerManager.ApplicationPools.Add(defaultName);
            options(new ApplicationPoolConfigurer(applicationPool));
            return this;
        }

        public IWebServerConfigurer CreateWebsite(Action<IWebsiteConfigurer> options)
        {
            var defaultSiteName = string.Format("Site{0}", ServerManager.Sites.Count + 1);
            var uniquePath = string.Format("/{0}", Guid.NewGuid().ToString("N"));
            var site = ServerManager.Sites.Add(defaultSiteName, uniquePath, 80);
            options(new WebsiteConfigurer(site));
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

        public IDeleteApplicationConfigurer DeleteApplication(string name)
        {                        
            throw new NotImplementedException();
        }

        public IWebServerConfigurer AlterWebsite(string name, Action<IWebsiteConfigurer> options)
        {
            throw new NotImplementedException();
        }

    }
}