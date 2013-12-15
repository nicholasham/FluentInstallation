using System;

namespace FluentInstallation.IIS
{
    public interface IWebServerConfigurer : IRootConfigurer
    {
        IWebServerConfigurer CreateApplicationPool(Action<IApplicationPoolConfigurer> configurer);
        IWebServerConfigurer CreateWebsite(Action<IWebsiteConfigurer> configurer);

        IWebServerConfigurer DeleteApplicationPool(string name);
        IWebServerConfigurer DeleteWebsite(string name);
        
        IWebServerConfigurer AlterWebsite(string name, Action<IWebsiteConfigurer> configurer);
    }
}