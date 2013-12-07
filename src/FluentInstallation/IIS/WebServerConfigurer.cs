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

        public IWebServerConfigurer CreateApplicationPool(Action<IApplicationPoolOptions> options)
        {
            var applicationPool = ServerManager.ApplicationPools.Add(Guid.NewGuid().ToString());
            options(new ApplicationPoolOptions(applicationPool));
            return this;
        }

        public IWebServerConfigurer CreateWebsite(Action<IWebsiteOptions> options)
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

        public IDeleteApplicationOptions DeleteApplication(string name)
        {
            throw new NotImplementedException();
        }

        public IWebServerConfigurer AlterWebsite(string name, Action<IWebsiteOptions> options)
        {
            throw new NotImplementedException();
        }

    }
}