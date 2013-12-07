using System;
using Microsoft.Web.Administration;

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
            throw new NotImplementedException();
        }

        public IWebServerConfigurer DeleteApplicationPool(string name)
        {
            throw new NotImplementedException();
        }

        public IWebServerConfigurer DeleteWebsite(string name)
        {
            throw new NotImplementedException();
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